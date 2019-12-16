


$(document).on('click', '.btnEliminarCarrito', function () {

    let iIdCarrito = $(this).attr('idCarrito');

    QuitarProductoDeCarrito(iIdCarrito, $(this).closest("tr"));


});


$(document).ready(function () {

    leerRadioButtonPago();

    ObtenerTotalApagar();


});

/**
 * FUNCIÓN QUE SIRVE PARA LEER EL VALOR DE UN RADIO BUTTON SELECCIONADO.
 * */
function leerRadioButtonPago() {

    $("#btnPagar").click(function () {

        let radioButtonSeleccionado = document.querySelector('input[name=inlineRadioOptions]:checked')

        if (radioButtonSeleccionado != null) {

            let radioButtonValor = radioButtonSeleccionado.value
           

            if (radioButtonValor == "Paypal") {

                let Productos = ObtenerProductos();

                ConectarPagoPaypal("POST", "PagoConPaypal", Productos, "JSON", "application/json; charset=utf-8").then((objRespuesta) => {

                    if (objRespuesta._bEstadoOperacion == true) {

                        window.location.replace(objRespuesta._cPaypalRedirectUrl);

                    }
                    else {

                        llamarSwetalert(objRespuesta);

                    }

                })

            }

            if (radioButtonValor == "Tarjeta") {

                let Productos = ObtenerProductos();

                let iCantidad = $("#Total").html();

                sessionStorage.setItem('productos', Productos);
                sessionStorage.setItem('cantidad', iCantidad);

                window.location.href = "/Cliente/CarritoCompras/PagoConTarjeta";
            }

        }



    })

    $("input[name='inlineRadioOptions']").click(function () {

        $("#btnPagar").removeClass("disabled")

    })

}

/**
 * MÉTODO PARA OBTENER LOS PRODUCTOS AGREGADOS AL CARRITO.
 * */
function ObtenerProductos() {

    let lstProducto = []

    let Productos = {
        lstProducto
    }

    $(".NumeroPedido").each(function () {

        let iIdProducto = $(this).attr("id")

        let iCantidad = $(this).val()

        lstProducto.push({ iIdProducto: iIdProducto, iCantidad: iCantidad })

    })


    Productos = JSON.stringify(Productos);

    return Productos;

  }

/**
 * MÉTODO PARA OBTNER EL PRECIO TOTAL DE LOS PRODUCTOS EN CARRITO
 * */
function ObtenerTotalApagar() {

    let dPrecio = 0

    $(".precioProducto").each(function () {

        dPrecio += parseInt($(this).html());
    });

    $("#SubTotal").html(dPrecio);
    $("#Total").html(dPrecio);

}


/**
 * MÉTODO PARA DISMINUIR LA CANTIDAD DE PEDIDO DEL PRODUCTO.
 * */
$(".aumentarCantidad").click(function () {
    let cElemento = $(this).siblings('.NumeroPedido');
    let iValor = parseInt(cElemento.val());

    if (iValor > 1 ) cElemento.val(iValor - 1);
   
});

/**
 * MÉTODO PARA AUMENTAR LA CANTIDAD DE PEDIDO DEL PRODUCTO.
 * */
$(".disminuirCantidad").click(function () {
    let cElemento = $(this).siblings('.NumeroPedido');
    let iValor = parseInt(cElemento.val());
    let iCantidadDisponible = parseInt($(this).attr('cantidadDisponible'));
    console.log(iCantidadDisponible);
    if (iValor < iCantidadDisponible) cElemento.val(iValor + 1);

});