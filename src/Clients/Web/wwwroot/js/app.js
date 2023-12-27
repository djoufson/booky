/* Navbar behaviour */
function onHamburgerClicked(){
    const hamburger = document.getElementById("hamburger");
    const menu = document.getElementById("menu");

    hamburger.classList.toggle("active"); // Change the image to cross or hamburger
    menu.classList.toggle("active"); // Show or hide the menu
}
