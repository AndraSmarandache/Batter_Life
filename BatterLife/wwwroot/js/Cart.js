let cart = JSON.parse(localStorage.getItem('cart')) || [];

function addToCart(product) {
    const existingItem = cart.find(item => item.id === product.id);
    if (existingItem) {
        existingItem.quantity += 1; 
    } else {
        cart.push({ ...product, quantity: 1 }); 
    }
    localStorage.setItem('cart', JSON.stringify(cart)); 
    alert(`${product.name} added to cart!`);
}

function getCart() {
    return cart;
}

function updateCart(newCart) {
    cart = newCart;
    localStorage.setItem('cart', JSON.stringify(cart));
}

function removeFromCart(productId) {
    cart = cart.filter(item => item.id !== productId); 
    localStorage.setItem('cart', JSON.stringify(cart));
}

function clearCart() {
    cart = [];
    localStorage.setItem('cart', JSON.stringify(cart));
}

export { addToCart, getCart, updateCart, removeFromCart, clearCart };
const overlay = document.getElementById('overlay');
const cartSidebar = document.getElementById('cart-sidebar');
const openCartButton = document.getElementById('open-cart');
const closeCartButton = document.getElementById('close-cart');

function openCart() {
    cartSidebar.classList.add('active'); 
    overlay.style.display = 'block'; 
}

function closeCart() {
    cartSidebar.classList.remove('active');
    overlay.style.display = 'none'; 
}

openCartButton.addEventListener('click', openCart);

closeCartButton.addEventListener('click', closeCart);

overlay.addEventListener('click', closeCart);