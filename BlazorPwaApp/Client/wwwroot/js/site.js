document.addEventListener('DOMContentLoaded', function () {
   var userDropdown = document.getElementById('userDropdown');
   userDropdown.addEventListener('click', function (event) {
      event.preventDefault();
      var dropdownMenu = userDropdown.nextElementSibling;
      dropdownMenu.classList.toggle('show');
   });
});