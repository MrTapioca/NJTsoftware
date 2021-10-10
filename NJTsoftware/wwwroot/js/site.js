// Initialize the hyphenating functionality
$('.hyphenate').hyphenate('en-us');

// Fade in page after images are loaded
$('#body-content').imagesLoaded()
    .always(function () {
        $('#load-spinner').fadeOut();
        $('#body-content').addClass('fade-in');
    });

// Enable scroll to section animation
$(function () {
    $('.navbar a, a[href="#contact"]').on('click', function (event) {

        if (this.hash !== '') {
            event.preventDefault();

            $('html,body').animate({ scrollTop: $(this.hash).offset().top }, function () {
                //$('.navbar-collapse').collapse('hide');
            });
        }
    });
});

// Insert email from data tags
$(function () {
    var $emailEl = $('[data-insert="email"][data-user][data-domain]');
    $emailEl.each(function () {
        var data = $(this).data();
        $(this).html(`<a href="mailto:${data.user}@${data.domain}">${data.user}@${data.domain}</a>`);
    });
});

// Lightbox functionality
var $lightbox = $('.lightbox');
var $body = $('body');

$('.portfolio-thumbnail').click(function () {
    openLightbox($(this).attr('src'));
});

function openLightbox(image) {
    $lightbox.find('img').attr('src', image);
    $lightbox.addClass('lightbox-show');
}

$lightbox.click(function () {
    $lightbox.removeClass('lightbox-show');
});

// Contact form functionality
$(function () {
    var $contactForm = $('#contact-form');
    //var $submitButton = $contactForm.find('button[data-submit="true"]');
    var $submitButton = $contactForm.find('button[type=submit]');
    var $sent = $('#contact-form-sent');
    var $error = $('#contact-form-error');

    //$sent.hide();
    $sent.find('button.close').click(function () {
        $sent.fadeOut();
    });

    //$error.hide();
    $error.find('button.close').click(function () {
        $error.fadeOut();
    });

    //$submitButton.find('span[data-sending="true"]').hide();

    $contactForm.submit(function (e) {
        //$contactForm.validate();
        e.preventDefault();

        //var buttonWidth = $submitButton.width();
        $submitButton.find('[data-sending=false]').hide();
        $submitButton.find('[data-sending=true]').show();
        $submitButton.attr('disabled', '');

        //if ($contactForm.valid()) {
        $.ajax({
            url: 'contact/send',
            type: 'POST',
            data: $contactForm.serialize()
        }).done(function () {
            //$contactForm.clearForm();
            $contactForm[0].reset();
            $sent.fadeIn();
        }).fail(function () {
            $error.fadeIn();
        }).always(function () {
            $submitButton.removeAttr('disabled');
            //$submitButton.find('span[data-sending="false"]').css('visibility', 'visible');
            $submitButton.find('[data-sending=true]').hide();
            $submitButton.find('[data-sending=false]').show();
        });
        //}
    });
});