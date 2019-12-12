$(document).ready(function () {

    leerRadioButtonPago()

});

/**
 * Función que sirve para leer el valor de un radio button seleccionado.
 * */
function leerRadioButtonPago() {

    $("#btnPagar").click(function () {

        let radioButtonSeleccionado = document.querySelector('input[name=inlineRadioOptions]:checked')

        if (radioButtonSeleccionado != null) {

            let radioButtonValor = radioButtonSeleccionado.value
            console.log(radioButtonValor)

            if (radioButtonValor == "Paypal") {

                ObtenerProductos()

            }

            if (radioButtonValor == "Tarjeta") {

            }

        }



    })

    $("input[name='inlineRadioOptions']").click(function () {

        $("#btnPagar").removeClass("disabled")

    })

}

function ObtenerProductos() {

    let lstProducto = []

    let Productos = {
        lstProducto
    }

    $(".CartaProducto").each(function () {

        let iIdProducto = $(this).attr("idProducto")

        let iCantidad = $(this).find(".NumeroPedido").val()

        lstProducto.push({ iIdProducto: iIdProducto, iCantidad: iCantidad })

    })


    Productos = JSON.stringify(Productos);

    ObtenerMetodoControlador("POST", "PagoConPaypal", Productos, "JSON", "application/json; charset=utf-8").then((objRespuesta) => {

        if (objRespuesta._bEstadoOperacion == true) {

            window.location.replace(objRespuesta._cPaypalRedirectUrl);

        }
        else {

            llamarSwetalert(objRespuesta);

        }

    })

}
