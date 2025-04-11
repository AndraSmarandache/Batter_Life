// Initialize when DOM is loaded
document.addEventListener('DOMContentLoaded', function () {
    // Get the cart count from a data attribute
    const cartCountElement = document.getElementById('cart-count');
    const initialCount = cartCountElement ? parseInt(cartCountElement.dataset.count) || 0 : 0;

    // Quantity change handlers
    document.querySelectorAll('.decrease-quantity').forEach(function (button) {
        button.addEventListener('click', function () {
            const input = this.nextElementSibling;
            if (parseInt(input.value) > 1) {
                input.value = parseInt(input.value) - 1;
                input.dispatchEvent(new Event('change'));
            }
        });
    });

    document.querySelectorAll('.increase-quantity').forEach(function (button) {
        button.addEventListener('click', function () {
            const input = this.previousElementSibling;
            input.value = parseInt(input.value) + 1;
            input.dispatchEvent(new Event('change'));
        });
    });

    // Auto-submit when quantity changes
    document.querySelectorAll('input[name="quantity"]').forEach(function (input) {
        input.addEventListener('change', function () {
            this.closest('form').querySelector('.update-button').click();
        });
    });

    // Update cart count in header
    function updateCartCount() {
        if (cartCountElement) {
            // This would ideally be updated via AJAX after cart operations
            // For now, we'll just use the initial count
            cartCountElement.textContent = initialCount;
        }
    }

    updateCartCount();
});