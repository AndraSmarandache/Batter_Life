document.querySelectorAll('.form-group input, .form-group textarea').forEach(input => {
  input.addEventListener('focus', () => {
      input.parentElement.style.transform = 'scale(1.02)';
  });
  input.addEventListener('blur', () => {
      input.parentElement.style.transform = 'scale(1)';
  });
});