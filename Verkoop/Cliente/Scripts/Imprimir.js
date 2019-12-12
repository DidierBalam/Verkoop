/**Función para inicar botón */
$(document).ready(function () {
    Boton();

});
/**Función para descargar un pdf con los datos de la compra presionar el botón Imprimir*/
function Boton() {
    $(".btnImprimir").click(function (e) {

        e.preventDefault();

        let iIdCompra = $(this).attr("iIdCompra");

        console.log(iIdCompra);

        window.location.href = "../HistorialCompras/ImprimirTicketDeCompra?iIdCompra=" + iIdCompra;

    });
}