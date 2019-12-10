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

    //$("#btnEliminarTargeta").click(function (e) {
    //    e.preventDefault();

    //    let _iIdTarjeta;

    //    if (_iIdTarjeta) {
    //        Swal.fire({
    //            title: '¿Esta seguro de eliminarlo?',
    //            type: 'warning',
    //            showCancelButton: true,
    //            confirmButtonColor: '#3085d6',
    //            cancelButtonColor: '#d33',
    //            confirmButtonText: 'Si, !Eliminalo!',
    //            cancelButtonText: 'Cancelar'
    //        }).then((result) => {
    //            if (result.value) {
    //                LlamarMetodo("POST", "../Tarjeta/EliminarTarjeta", { _iIdTarjeta: _iIdTarjeta });
    //                Swal.fire(
    //                    'Eliminar!',
    //                    'Ha sido eliminada.',
    //                    'Aceptar.'
    //                )
    //            }
    //        })
    //    }

    //});
    $(".btnEliminarTarjeta").click(function (e) {
        e.preventDefault();
        console.log($(this).attr("idTarjeta"));

    });

}

/**esta función le da funcionalidad al botón para guardar tarjeta  */
function GuardarTarjeta() {
    $("#btnGuardarTarjeta").click(function (e) {
        e.preventDefault();
        //alert('You clicked the button!')
        if ($('#FormularioTarjeta').valid()) {
            ObtenerDatosTarjeta();

            $('#ModalPrincipal').modal('hide'); //oculta el modal

        }

    });

}

/**esta función trae los datos de la tarjeta y contiene el método que contiene la URL con los datos */
function ObtenerDatosTarjeta() {
    var Data = {}

    var Tarjeta = {
        cNumeroTarjeta: $("#cNumeroTarjeta").val(),
        cMesVigencia: $("#cMesVigencia").val(),
        cAnioVigencia: $("#cAnioVigencia").val()
    };

    Data['Tarjeta'] = JSON.stringify(Tarjeta);// convierte a una cadena JSON

    LlamarMetodo("POST", "..//Tarjeta/GuardarTarjeta", Data);
};




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



