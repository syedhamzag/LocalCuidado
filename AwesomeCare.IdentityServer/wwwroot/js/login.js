    $(function () {
        { /* Desktop Home : Everything homepage in this block */
            var loginForm = $('#loginForm'),
                companyHrms = $('.company-hrms'),
                callToActionBtn = $('#callToActionBtn');

            // windows.bgImages initialized in the ../view/pages/elements/head-content.blade.php file
            if (window.bgImages.length) {
                $('#home').backstretch(window.bgImages, {duration: 3000, fade: 750});
            }

            if (localStorage.getItem('showLoginForm') === 'true') {
                // hide $(this)
                callToActionBtn.fadeOut(function () {
                    // hide Welcome Heading Note
                    if (companyHrms.length) {
                        $('.welcome-heading').fadeOut(function () {
                            // show login form
                            loginForm.fadeIn();
                        });
                    } else {
                        loginForm.fadeIn();
                    }
                });
            }

            callToActionBtn.click(function () {
                // hide $(this)
                $(this).fadeOut(function () {
                    // hide Welcome Heading Note
                    if (companyHrms.length) {
                        $('.welcome-heading').fadeOut(function () {
                            // show login form
                            loginForm.fadeIn();
                        });
                    } else {
                        loginForm.fadeIn();
                    }
                });

                // ClientSettings : save state in the browser
                if (typeof (Storage) !== "undefined") {
                    localStorage.setItem('showLoginForm', 'true');
                }
            });

            $('a#closeButton').click(function () {
                // hide login form
                $(this).parent(loginForm).fadeOut(function () {
                    // show Welcome Heading Note
                    if (companyHrms.length) {
                        $('.welcome-heading').fadeIn(function () {
                            // show call to action button
                            callToActionBtn.fadeIn();
                        });
                    } else {
                        callToActionBtn.fadeIn();
                    }
                });

                // ClientSettings : save state in the browser
                if (typeof (Storage) !== "undefined") {
                    localStorage.setItem('showLoginForm', 'false');
                }
            });
        }

        { /* Mobile Home : Everything homepage in this block */
            var loginFormMobile = $('#loginFormMobile'),
                callToActionBtnMobile = $('#callToActionBtnMobile'),
                whistleBtn = $('#modalButton.whistle-button');

            if (localStorage.getItem('showLoginFormMobile') === 'true') {
                whistleBtn.hide();
                callToActionBtnMobile.fadeOut(function () {
                    loginFormMobile.fadeIn();
                });
            }

            callToActionBtnMobile.click(function () {
                // hide $(this)
                whistleBtn.hide();
                $(this).fadeOut(function () {
                    loginFormMobile.fadeIn();
                });

                // ClientSettings : save state in the browser
                if (typeof (Storage) !== "undefined") {
                    localStorage.setItem('showLoginFormMobile', 'true');
                }
            });

            $('a#closeButtonMobile').click(function () {
                // hide login form
                $(this).parent(loginFormMobile).fadeOut(function () {
                    callToActionBtnMobile.fadeIn();
                    whistleBtn.show();
                });

                // ClientSettings : save state in the browser
                if (typeof (Storage) !== "undefined") {
                    localStorage.setItem('showLoginFormMobile', 'false');
                }
            });
        }

    });
