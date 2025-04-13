document.addEventListener('DOMContentLoaded', function () {
    let quantity = 1;
    const quantityInput = document.getElementById('quantity');
    const priceElement = document.querySelector('.price');
    const addToCartButton = document.querySelector('.add-to-cart-button');
    const productDetailsContent = document.querySelector('.product-details-content');

    if (!productDetailsContent) {
        console.error('Product details content not found');
        return;
    }

    let productId = productDetailsContent.dataset.productId;
    if (!productId) {
        const match = window.location.pathname.match(/\/ProductDetails\/Index\/(\d+)/);
        if (match) productId = match[1];
    }

    if (!productId) {
        console.error('Product ID not found');
        return;
    }

    const productName = document.querySelector('.product-name')?.textContent;
    const productImage = document.querySelector('.product-details-image')?.src;

    const originalPriceText = document.querySelector('.price')?.textContent;
    const originalPrice = originalPriceText ? parseFloat(originalPriceText.replace(/[^0-9.]/g, '')) : 0;

    if (isNaN(originalPrice)) {
        console.error('Could not parse product price');
        return;
    }

    function updateTotalPrice() {
        const totalPrice = originalPrice * quantity;
        if (priceElement) {
            priceElement.innerHTML = `<strong>Price:</strong> $${totalPrice.toFixed(2)}`;
        }
    }

    updateTotalPrice();

    document.querySelector('.decrease-quantity')?.addEventListener('click', function () {
        if (quantity > 1) {
            quantity--;
            if (quantityInput) quantityInput.value = quantity;
            updateTotalPrice();
        }
    });

    document.querySelector('.increase-quantity')?.addEventListener('click', function () {
        quantity++;
        if (quantityInput) quantityInput.value = quantity;
        updateTotalPrice();
    });

    if (quantityInput) {
        quantityInput.addEventListener('change', function () {
            const newQuantity = parseInt(this.value);
            if (!isNaN(newQuantity) && newQuantity >= 1) {
                quantity = newQuantity;
                updateTotalPrice();
            } else {
                this.value = quantity;
            }
        });
    }

    if (addToCartButton) {
        addToCartButton.addEventListener('click', async function (e) {
            e.preventDefault();

            if (!productName || !productId) {
                console.error('Missing product information');
                return;
            }

            const productData = {
                id: parseInt(productId),
                name: productName,
                price: originalPrice,
                imageUrl: productImage || ''
            };

            try {
                const added = await addToCart(productData, quantity);
                if (added) {
                    const notification = document.createElement('div');
                    notification.className = 'cart-notification';
                    notification.textContent = `${quantity} ${productName} added to cart!`;
                    document.body.appendChild(notification);

                    setTimeout(() => {
                        notification.remove();
                    }, 2000);
                }
            } catch (error) {
                console.error('Error adding to cart:', error);
                const notification = document.createElement('div');
                notification.className = 'cart-notification error';
                notification.textContent = 'Error adding to cart. Please try again.';
                document.body.appendChild(notification);

                setTimeout(() => {
                    notification.remove();
                }, 2000);
            }
        });
    }

    const seeReviewsButton = document.querySelector('.see-reviews-button');
    const reviewsContainer = document.querySelector('.reviews');

    if (seeReviewsButton && reviewsContainer) {
        seeReviewsButton.addEventListener('click', function () {
            if (reviewsContainer.style.display === 'none' || !reviewsContainer.style.display) {
                reviewsContainer.style.display = 'block';
                this.textContent = 'Hide Reviews';
            } else {
                reviewsContainer.style.display = 'none';
                this.textContent = 'See Reviews';
            }
        });
    }
});