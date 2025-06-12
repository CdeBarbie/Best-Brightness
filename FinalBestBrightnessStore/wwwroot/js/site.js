// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
window.onload = function () {
    function updateTime() {
        const timer = document.getElementById('timer');
        const now = new Date();
        const formattedTime = now.toLocaleTimeString();
        timer.textContent = formattedTime;
    }

    updateTime(); // Initial call
    setInterval(updateTime, 1000); // Update every second
};