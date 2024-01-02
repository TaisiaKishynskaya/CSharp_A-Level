$(window).on('beforeunload', function () {
    localStorage.setItem('scrollPosition', $(window).scrollTop());
});

$(document).ready(function () {
    var scrollPosition = localStorage.getItem('scrollPosition');
    if (scrollPosition) {
        $(window).scrollTop(scrollPosition);
        localStorage.removeItem('scrollPosition');
    }
});