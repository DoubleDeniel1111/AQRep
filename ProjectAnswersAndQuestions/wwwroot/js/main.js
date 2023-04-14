(function($) {

    $(".toggle-password").click(function() {

        $(this).toggleClass("zmdi-eye zmdi-eye-off");
        var input = $($(this).attr("toggle"));
        if (input.attr("type") == "password") {
          input.attr("type", "text");
        } else {
          input.attr("type", "password");
        }
      });

})(jQuery);


function validation(form) {

    function removeError(input) {
        const parent = input.parentNode;
        if (parent.classList.contains('error')) {
            parent.querySelector('.errorLabel').remove();
            parent.classList.remove('error')
        }
    }

    function createError(input, text) {
        const parent = input.parentNode;
        const errorLabel = document.createElement('Label');
        errorLabel.classList.add('errorLabel');

        errorLabel.textContent = text;

        parent.classList.add('error');

        parent.append(errorLabel);
    }

    let result = true

    const Inputs = form.querySelectorAll('input');

    for (const input of Inputs) {

        removeError(input);
        if (input.value == "") {
            console.log('error');
            createError(input,'field is null');
            result = false
        }
    }

    return result;
}

//document.getElementById('signup-form').addEventListener('submit', function (event) {

//    event.preventDefault()

//    if (validation(this)) {
//        alert('OK');

//    }

   

//});