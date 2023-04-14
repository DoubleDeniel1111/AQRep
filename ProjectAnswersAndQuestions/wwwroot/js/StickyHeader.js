window.onscroll = function () { myFunction() };

var header = document.getElementById("myHeader");
var header2 = document.getElementById("myHeader2");
var sticky = header.offsetTop;

function myFunction() {
    if (window.pageYOffset >= sticky) {
        header.classList.add("sticky");
        header2.classList.add("sticky");
    } else {
        header.classList.remove("sticky");
        header2.classList.remove("sticky");
    }
}



