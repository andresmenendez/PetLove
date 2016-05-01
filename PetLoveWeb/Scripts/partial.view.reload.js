/*
* Objetivo da função a seguir é atualizar e validar o model de view partial
* fazendo com que a só a view partial seja atualizada, dessa forma não ocasiona
* reload da pagina nem perda visual do dados preenchidos em outras view partial
*/
$(function () {
    $('form').submit(function () {
        if ($(this).valid()) {
            $.ajax({
                url: this.action,
                type: this.method,
                data: $(this).serialize(),
                success: function (result) {
                    $('#result').html(result);
                }
            });
        }
        return false;
    });
});