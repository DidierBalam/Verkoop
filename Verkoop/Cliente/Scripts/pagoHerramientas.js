$(document).ready(function () {


    let iCantidad = sessionStorage.getItem('cantidad')

    $("#precio").val('$' + iCantidad);
})

$("#btnPagar").click(function () {

    
    RealizarPagoConTarjeta(ArmarObjetoPago());
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


