document.addEventListener('DOMContentLoaded', function () {
    const images = document.querySelectorAll('.about-content img');
    const nextButton = document.querySelector('.next-button');
    const prevButton = document.querySelector('.prev-button');
    let currentIndex = 0;

    function showImage(index) {
        images.forEach((img, i) => {
            if (i === index) {
                img.style.display = 'block';
            } else {
                img.style.display = 'none';
            }
        });
    }

    function showNextImage() {
        currentIndex = (currentIndex + 1) % images.length;
        showImage(currentIndex);
    }

    function showPrevImage() {
        currentIndex = (currentIndex - 1 + images.length) % images.length;
        showImage(currentIndex);
    }

    if (nextButton && prevButton) {
        nextButton.addEventListener('click', showNextImage);
        prevButton.addEventListener('click', showPrevImage);
    }

    showImage(currentIndex);
});