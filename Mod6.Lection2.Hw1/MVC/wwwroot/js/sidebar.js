$(document).ready(function () {
    $('.sidebarOpen').click(function () {
        $('#sidebarContentClone').html($('#sidebarContent').html());
        $('#sidebar').fadeIn();
    });
    $('#sidebarClose').click(function () {
        $('#sidebar').fadeOut();
    });
}); 