document.addEventListener('DOMContentLoaded', function () {
    const overlay = document.getElementById('overlay');
    const cartSidebar = document.getElementById('cart-sidebar');
    const openCartButton = document.getElementById('open-cart');
    const closeCartButton = document.getElementById('close-cart');
    const cartItemsContainer = document.getElementById('sidebar-cart-items');
    const cartCount = document.querySelector('.cart-count');
    const subtotalElement = document.getElementById('subtotal');
    const shippingElement = document.getElementById('shipping');
    const totalElement = document.getElementById('total');
    const checkoutButton = document.querySelector('.checkout-button');
    const continueShoppingButton = document.getElementById('continue-shopping');

    let cart = [];
    let scrollPosition = 0;

    initCart();

    async function initCart() {
        await loadCartFromServer();
        updateCartDisplay();
        await updateCartCount();
    }

    async function loadCartFromServer() {
        try {
            const response = await fetch('/Cart/GetCartItems', {
                credentials: 'include'
            });

            if (response.ok) {
                const data = await response.json();
                cart = Array.isArray(data.items) ? data.items : [];
            }
        } catch (error) {
            cart = [];
        }
    }

    function openCart() {
        scrollPosition = window.pageYOffset;
        document.body.style.top = `-${scrollPosition}px`;
        document.body.classList.add('no-scroll');
        overlay.style.display = 'block';
        setTimeout(() => {
            overlay.style.opacity = '1';
            cartSidebar.classList.add('active');
        }, 10);
        updateCartDisplay();
    }

    function closeCart() {
        cartSidebar.classList.remove('active');
        overlay.style.opacity = '0';
        setTimeout(() => {
            overlay.style.display = 'none';
            document.body.classList.remove('no-scroll');
            window.scrollTo(0, scrollPosition);
            document.body.style.top = '';
        }, 300);
    }

    function updateCartDisplay() {
        cartItemsContainer.innerHTML = '';

        if (!cart || cart.length === 0) {
            cartItemsContainer.innerHTML = '<div class="empty-cart">Your cart is empty</div>';
            subtotalElement.textContent = '$0.00';
            shippingElement.textContent = '$0.00';
            totalElement.textContent = '$0.00';
            return;
        }

        let subtotal = 0;
        cart.forEach(item => {
            const itemTotal = parseFloat(item.price) * parseInt(item.quantity);
            subtotal += itemTotal;

            const cartItemElement = document.createElement('div');
            cartItemElement.className = 'cart-item';
            cartItemElement.innerHTML = `
                <img src="${item.image || '/images/placeholder.png'}" alt="${item.name || 'Product'}">
                <div class="cart-item-details">
                    <h3>${item.name || 'Unknown Product'}</h3>
                    <p class="price">$${(parseFloat(item.price) || 0).toFixed(2)}</p>
                    <div class="quantity-selector">
                        <button class="decrease" data-id="${item.id}">-</button>
                        <input type="number" value="${item.quantity}" min="1" data-id="${item.id}">
                        <button class="increase" data-id="${item.id}">+</button>
                    </div>
                </div>
                <button class="remove-item" data-id="${item.id}">
                    <i class="fas fa-trash"></i>
                </button>
            `;
            cartItemsContainer.appendChild(cartItemElement);
        });

        const shipping = subtotal > 50 ? 0 : 5;
        const total = subtotal + shipping;

        subtotalElement.textContent = `$${subtotal.toFixed(2)}`;
        shippingElement.textContent = `$${shipping.toFixed(2)}`;
        totalElement.textContent = `$${total.toFixed(2)}`;

        addCartItemEventListeners();
    }

    async function updateCartCount() {
        try {
            const response = await fetch('/Cart/GetCartItems', {
                credentials: 'include'
            });

            if (response.ok) {
                const data = await response.json();
                const count = Array.isArray(data.items)
                    ? data.items.reduce((total, item) => total + (parseInt(item.quantity) || 0), 0)
                    : 0;
                document.querySelectorAll('.cart-count').forEach(el => {
                    el.textContent = count;
                });
            }
        } catch (error) {
            console.error('Error updating cart count:', error);
        }
    }

    function addCartItemEventListeners() {
        document.querySelectorAll('.decrease').forEach(button => {
            button.addEventListener('click', decreaseQuantity);
        });
        document.querySelectorAll('.increase').forEach(button => {
            button.addEventListener('click', increaseQuantity);
        });
        document.querySelectorAll('.remove-item').forEach(button => {
            button.addEventListener('click', removeItem);
        });
        document.querySelectorAll('.quantity-selector input').forEach(input => {
            input.addEventListener('change', updateQuantity);
        });
    }

    async function decreaseQuantity(e) {
        const id = e.target.getAttribute('data-id');
        const response = await fetch(`/Cart/UpdateItem?productId=${id}&quantity=-1`, {
            method: 'POST',
            credentials: 'include'
        });
        if (response.ok) {
            await loadCartFromServer();
            updateCartDisplay();
            await updateCartCount();
        }
    }

    async function increaseQuantity(e) {
        const id = e.target.getAttribute('data-id');
        const response = await fetch(`/Cart/UpdateItem?productId=${id}&quantity=1`, {
            method: 'POST',
            credentials: 'include'
        });
        if (response.ok) {
            await loadCartFromServer();
            updateCartDisplay();
            await updateCartCount();
        }
    }

    async function updateQuantity(e) {
        const id = e.target.getAttribute('data-id');
        const newQuantity = parseInt(e.target.value);
        if (newQuantity > 0) {
            const response = await fetch(`/Cart/UpdateItem?productId=${id}&quantity=${newQuantity}`, {
                method: 'POST',
                credentials: 'include'
            });
            if (response.ok) {
                await loadCartFromServer();
                updateCartDisplay();
                await updateCartCount();
            }
        } else {
            e.target.value = 1;
        }
    }

    async function removeItem(e) {
        const id = e.target.closest('button').getAttribute('data-id');
        const response = await fetch(`/Cart/RemoveItem?productId=${id}`, {
            method: 'POST',
            credentials: 'include'
        });
        if (response.ok) {
            await loadCartFromServer();
            updateCartDisplay();
            await updateCartCount();
        }
    }

    window.addToCart = async function (product, quantity = 1) {
        try {
            const response = await fetch('/Cart/AddItem', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                credentials: 'include',
                body: JSON.stringify({
                    productId: product.id,
                    quantity: quantity
                })
            });

            if (!response.ok) {
                throw new Error('Network response was not ok');
            }

            const data = await response.json();

            if (data.success) {
                await loadCartFromServer();
                updateCartDisplay();
                updateCartCount();
                openCart();

                showNotification(`${product.name} added to cart!`);
                return true;
            } else {
                showNotification(data.message || "Failed to add to cart", 'error');
                return false;
            }
        } catch (error) {
            showNotification("Error adding to cart. Please try again.", 'error');
            return false;
        }
    };

    function showNotification(message, type = 'success') {
        const notification = document.createElement('div');
        notification.className = `cart-notification ${type}`;
        notification.textContent = message;
        document.body.appendChild(notification);

        setTimeout(() => {
            notification.remove();
        }, 3000);
    }

    checkoutButton.addEventListener('click', function () {
        fetch('/Cart/Checkout', {
            method: 'POST',
            credentials: 'include'
        }).then(response => {
            if (response.ok) {
                window.location.href = '/Cart/Index';
            }
        });
    });

    openCartButton.addEventListener('click', function (e) {
        e.preventDefault();
        openCart();
    });

    closeCartButton.addEventListener('click', closeCart);
    overlay.addEventListener('click', closeCart);
    continueShoppingButton.addEventListener('click', closeCart);
});