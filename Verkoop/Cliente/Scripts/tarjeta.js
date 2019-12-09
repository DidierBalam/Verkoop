$(document).ready(function () {
    IniciarBoton()
});

function IniciarBoton() {

    $("#btnAgregarTarjeta").click(function (e) {
        e.preventDefault();
        ObtenerVista("POST", "../Perfil/NuevaTarjeta");
        ValidarCampos("#FormularioTarjeta")     
    });

}

/** Validaciones*/
function ValidarCampos(ValidarFormulario) {
    $(ValidarFormulario).validate({
        rules: {
            NumeroTarjeta: {
                required: true,
                maxlength: 16,
                number: true
            },
            Mes: {
                required: true,
                number: true,
                maxlength: 2
            },
            Año: {
                required: true,
                number: true,
                maxlength: 4
            },
            Ccv: {
                required: true,
                number: true,
                maxlength: 3
            }
        },
    });
};

//valida mensaje
$.extend($.validator.messages, {
    required: "Este campo es requerido.",
    number: "Por favor introduzca un número válido.",
    maxlength: $.validator.format("Por favor solo ingrese hasta {0} caracteres."),
    minlength: $.validator.format("Por favor ingrese mínimo {0} caracteres.")
});



