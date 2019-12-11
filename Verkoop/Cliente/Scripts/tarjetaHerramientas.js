$(document).ready(function () {
    IniciarBoton();
});

/**esta función inicia el botón para agregar tarjeta */
function IniciarBoton() {
    $("#btnAgregarTarjeta").click(function (e) {
        e.preventDefault();
        ObtenerVista("POST", "../Perfil/NuevaTarjeta", null, 'GuardarTarjeta');
        ValidarCampos("#FormularioTarjeta")
    });


    $(".btnEliminarTarjeta").click(function (e) {
        e.preventDefault();
        EliminarTarjeta($(this).attr("idTarjeta"), $(this).parentsUntil(".-cardPadre"));
    });

}

/**esta función le da funcionalidad al botón para guardar tarjeta  */
function GuardarTarjeta() {
    $("#btnGuardarTarjeta").click(function (e) {
        e.preventDefault();
   
        if ($('#FormularioTarjeta').valid()) {

         var objTarjeta =  ObtenerDatosTarjeta();


            GuardarTarjeta("#ContenedorTarjetas", objTarjeta);
        }
        console.log(1);
    });

}

/**esta función trae los datos de la tarjeta y contiene el método que contiene la URL con los datos */
function ObtenerDatosTarjeta() {
  
    var objTarjeta = {
        cNumeroTarjeta: $("#cNumeroTarjeta").val(),
        cMesVigencia: $("#cMesVigencia").val(),
        cAnioVigencia: $("#cAnioVigencia").val()
    };

    return objTarjeta;
};


/** Esta función valida los campos con jquery validate*/
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

        },
    });
};

/*contiene los mensajes que seran llamados en la configuración de la función ValidarCampos*/
$.extend($.validator.messages, {
    required: "Este campo es requerido.",
    number: "Por favor introduzca un número válido.",
    maxlength: $.validator.format("Por favor solo ingrese hasta {0} caracteres."),
    minlength: $.validator.format("Por favor ingrese mínimo {0} caracteres.")
});



