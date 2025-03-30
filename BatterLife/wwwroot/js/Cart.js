document.addEventListener('DOMContentLoaded', function () {
    const overlay = document.getElementById('overlay');
    const cartSidebar = document.getElementById('cart-sidebar');
    const openCartButton = document.getElementById('open-cart');
    const closeCartButton = document.getElementById('close-cart');
    const cartItemsContainer = document.querySelector('.cart-items');
    const cartCount = document.querySelector('.cart-count');
    const subtotalElement = document.getElementById('subtotal');
    const shippingElement = document.getElementById('shipping');
    const totalElement = document.getElementById('total');
    const checkoutButton = document.querySelector('.checkout-button');
    const continueShoppingButton = document.getElementById('continue-shopping');

    const sampleProducts = [
        {
            id: 'prod1',
            name: 'Chocolate Cake',
            price: 24.99,
            image: '~/images/chocolate-cake.jpg',
            quantity: 1
        },
        {
            id: 'prod2',
            name: 'Tiramisu',
            price: 18,
            image: '~/images/tiramisu.jpg',
            quantity: 2
        }
    ];

    let cart = JSON.parse(localStorage.getItem('cart')) || sampleProducts;
    if (cart.length === 0) cart = sampleProducts;
    localStorage.setItem('cart', JSON.stringify(cart));
    let scrollPosition = 0;

    function initCart() {
        updateCartDisplay();
        updateCartCount();
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
        if (cart.length === 0) {
            cartItemsContainer.innerHTML = '<div class="empty-cart">Your cart is empty</div>';
            subtotalElement.textContent = '$0.00';
            shippingElement.textContent = '$0.00';
            totalElement.textContent = '$0.00';
            return;
        }

        let subtotal = 0;
        cart.forEach(item => {
            const itemTotal = item.price * item.quantity;
            subtotal += itemTotal;
            const cartItemElement = document.createElement('div');
            cartItemElement.className = 'cart-item';
            cartItemElement.innerHTML = `
                <img src="${item.image}" alt="${item.name}">
                <div class="cart-item-details">
                    <h3>${item.name}</h3>
                    <p class="price">$${(item.price * item.quantity).toFixed(2)}</p>
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
        shippingElement.textContent = subtotal > 50 ? '$0.00' : '$5.00';
        totalElement.textContent = `$${total.toFixed(2)}`;
        addCartItemEventListeners();
    }

    function updateCartCount() {
        const count = cart.reduce((total, item) => total + item.quantity, 0);
        cartCount.textContent = count;
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

    function decreaseQuantity(e) {
        const id = e.target.getAttribute('data-id');
        const item = cart.find(item => item.id === id);
        if (item.quantity > 1) {
            item.quantity--;
            updateCart();
        }
    }

    function increaseQuantity(e) {
        const id = e.target.getAttribute('data-id');
        const item = cart.find(item => item.id === id);
        item.quantity++;
        updateCart();
    }

    function updateQuantity(e) {
        const id = e.target.getAttribute('data-id');
        const item = cart.find(item => item.id === id);
        const newQuantity = parseInt(e.target.value);
        if (newQuantity > 0) {
            item.quantity = newQuantity;
            updateCart();
        } else {
            e.target.value = item.quantity;
        }
    }

    function removeItem(e) {
        const id = e.target.closest('button').getAttribute('data-id');
        cart = cart.filter(item => item.id !== id);
        updateCart();
    }

    function updateCart() {
        localStorage.setItem('cart', JSON.stringify(cart));
        updateCartDisplay();
        updateCartCount();
    }

    window.addToCart = function (product) {
        const existingItem = cart.find(item => item.id === product.id);
        if (existingItem) {
            existingItem.quantity++;
        } else {
            cart.push({ ...product, quantity: 1 });
        }
        updateCart();
        alert(`${product.name} added to cart!`);
    };

    checkoutButton.addEventListener('click', function () {
        alert('Proceeding to checkout');
    });

    openCartButton.addEventListener('click', function (e) {
        e.preventDefault();
        openCart();
    });

    closeCartButton.addEventListener('click', closeCart);
    overlay.addEventListener('click', closeCart);
    continueShoppingButton.addEventListener('click', closeCart);

    initCart();
});