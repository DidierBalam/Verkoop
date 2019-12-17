$(document).ready(function () {


    let iCantidad = sessionStorage.getItem('cantidad')

    $("#precio").val('$' + iCantidad);
})

$("#btnPagar").click(function () {

    if (PagoTarjetaValido()) {

        RealizarPagoConTarjeta(ArmarObjetoPago());

    }

    else {
        Swal.fire("Alerta", "Verifique el formulario", "warning")
    }
    
});

/**Validación para pagar con tarjeta */
function PagoTarjetaValido() {
    let oFormulario = $("#PagoTarjeta");
    oFormulario.validate({
        errorClass: "is-invalid",
        validClass: "is-valid",

        rules: {
            tarjeta: {
                required: true,
                minlength: 16,
            },
            mes: {
                required: true },
            anio: {
                required: true,
                minlength: 4,
            },
            ccv: {
                required: true,
                minlength: 3,
            }
           
        },
        messages: {
            tarjeta: {
                required: "Por favor ingrese el número de la tarjeta",
                minlength: "Porfavor ingrese 16 digitos"
            },
            mes: {
                required: ""
            },
            anio: {
                required: ""
            },
            ccv: {
                required: ""
            }
        }
    });
    return oFormulario.valid();
}


$('#tarjeta').on('input', function (e) {
    if (!/^[0123456789]*$/i.test(this.value)) {
        this.value = this.value.replace(/[^0123456789]+/ig, "");
    }
});

$('#mes').on('input', function (e) {
    if (!/^[0123456789]*$/i.test(this.value)) {
        this.value = this.value.replace(/[^0123456789]+/ig, "");
    }
});

$('#anio').on('input', function (e) {
    if (!/^[0123456789]*$/i.test(this.value)) {
        this.value = this.value.replace(/[^0123456789]+/ig, "");
    }
});

$('#ccv').on('input', function (e) {
    if (!/^[0123456789]*$/i.test(this.value)) {
        this.value = this.value.replace(/[^0123456789]+/ig, "");
    }
});


/**
 * FUNCIÓN PARA ARMAR EL OBJETO PAGO
 * */
function ArmarObjetoPago() {

    let productos = JSON.parse(sessionStorage.getItem('productos'));

    let objPago = {

        iIdDireccion: $("#direccion").attr('idDireccion'),
        objTarjeta: {
            iIdTarjeta: $("#direccion").attr('idTarjeta') == null ? 0 : $("#direccion").attr('idTarjeta'),
            cNumeroTarjeta: $("#direccion").val(),
            cMesVigencia: $("#mes").val(),
            cAnioVigencia: $("#anio").val()
        },

        lstProductoComprado: productos.lstProducto
        
        
    }

    return objPago;
}


