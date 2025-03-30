document.addEventListener('DOMContentLoaded', function () {
    let quantity = 1;
    const quantityInput = document.getElementById('quantity');
    const priceElement = document.querySelector('.price');

    const originalPriceText = document.querySelector('.price').textContent;
    const originalPrice = parseFloat(originalPriceText.replace(/[^0-9.]/g, ''));

    if (isNaN(originalPrice)) {
        console.error('Could not parse product price');
        return;
    }

    function updateTotalPrice() {
        const totalPrice = originalPrice * quantity;
        priceElement.innerHTML = `<strong>Price:</strong> $${totalPrice.toFixed(2)}`;
    }

    updateTotalPrice();

    document.querySelector('.decrease-quantity').addEventListener('click', function () {
        if (quantity > 1) {
            quantity--;
            quantityInput.value = quantity;
            updateTotalPrice();
        }
    });

    document.querySelector('.increase-quantity').addEventListener('click', function () {
        quantity++;
        quantityInput.value = quantity;
        updateTotalPrice();
    });

    quantityInput.addEventListener('change', function () {
        const newQuantity = parseInt(this.value);
        if (!isNaN(newQuantity) && newQuantity >= 1) {
            quantity = newQuantity;
            updateTotalPrice();
        } else {
            this.value = quantity;
        }
    });

    document.querySelector('.add-to-cart-button').addEventListener('click', function () {
        const productName = document.querySelector('.product-name').textContent;
        const totalPrice = originalPrice * quantity;
        alert(`Added ${quantity} ${productName} to cart. Total: $${totalPrice.toFixed(2)}`);
    });

    const seeReviewsButton = document.querySelector('.see-reviews-button');
    const reviewsContainer = document.querySelector('.reviews');

    seeReviewsButton.addEventListener('click', function () {
        if (reviewsContainer.style.display === 'none' || !reviewsContainer.style.display) {
            reviewsContainer.style.display = 'block';
            this.textContent = 'Hide Reviews';
        } else {
            reviewsContainer.style.display = 'none';
            this.textContent = 'See Reviews';
        }
    });
});