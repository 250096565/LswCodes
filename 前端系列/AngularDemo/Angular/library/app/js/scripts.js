
jQuery(document).ready(function () {

    /*
        Fullscreen background
    */
    $.backstretch("/app/img/1.jpg");

    /*
        Form validation
    */
    $('.login-form input[type="text"], .login-form input[type="password"], .login-form textarea').on('focus', function () {
        $(this).removeClass('input-error');
    });

    $('.login-form').on('submit', function (e) {

        var i = 1;
        var name = false;
        var pwd = false;
        $(this).find('input[type="text"], input[type="password"], textarea').each(function () {

            if (i == 1) {
                if ($(this).val() == "" || $(this).val() != "admin") {
                    e.preventDefault();
                    $(this).addClass('input-error');
                }
                else {
                    $(this).removeClass('input-error');
                    name = true;
                }
            }
            if (i == 2) {
                if ($(this).val() == "" || $(this).val() != "123456") {
                    e.preventDefault();
                    $(this).addClass('input-error');
                }
                else {
                    $(this).removeClass('input-error');
                    pwd = true;
                }
            }

            if (name && pwd) {
                window.location.href = "/Index.html#/Index";
            }
            i++;
        });

    });


});
