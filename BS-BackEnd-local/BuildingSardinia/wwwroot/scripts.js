document.addEventListener("DOMContentLoaded", function () {
    // Check authentication status (mock implementation)
    var isAuthenticated = false;
    var isSuperuser = false;
    var username = "JohnDoe";

    if (isAuthenticated) {
        document.getElementById("user-icon").innerHTML = '<i class="fas fa-user fa-lg"></i>';
        document.getElementById("user-name").innerText = username;
        document.getElementById("logout-link").style.display = 'block';
        document.getElementById("register-link").style.display = 'none';
        document.getElementById("login-link").style.display = 'none';
        if (isSuperuser) {
            document.getElementById("product-management").style.display = 'block';
        }
    }
});