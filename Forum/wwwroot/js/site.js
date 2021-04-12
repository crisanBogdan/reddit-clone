// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const navbarToggler = document.querySelector('.navbar-toggler');
const navbarList = document.querySelector('.navbar-collapse');

// on mobile
let navbarVisible = false;

navbarToggler.onclick = () => {
    navbarVisible = !navbarVisible;
    if (navbarVisible) {
        navbarList.classList.remove('collapse');
    } else {
        navbarList.classList.add('collapse');
    }
}

