let iNumeroConsulta = 1;

//LIGAS DE LOS FILTROS.
$(".TipoFiltro").click(function () {

    let cFiltro = $(this).attr('Filtro');

    if (cFiltro.toUpperCase() != "BUSCAR") window.location.href = "?_cFiltro=" + cFiltro;


});

//TECLEAR EN LA BARRA DE BÚSQUEDA.
$("#BarraBusqueda").keyup(function (e) {

    if (e.keyCode != 32 && e.keyCode != 8) {

        iNumeroConsulta = 0;

        CambiarFiltroAbuscarProducto();

        BuscarProducto($(this).val(), iNumeroConsulta)//Número de consulta siempre en 0
    };
});

//Botón que redirigea a la vista de inicar sesión.
$("#btnAcceder").click(function () {

    window.location.href = "Cliente/Sesion/Index";

});

//BOTÓN VER DEL PRODUCTO.
$("#contendorProductos").on('click', '.btnVerProducto', function () {

    VisualizarDetallesProducto($(this).attr('idProducto'));

});

//BOTÓN AGREGAR PRODUCTO.
$("#contendorProductos").on('click', '.btnAgregarCarrito', function () {

    AgregarProductoCarrito($(this).attr('idProducto'));

});

//FUNCIÓN PARA VERIFICAR CUANDO SE LLEGA AL FINAL DE LA PÁGINA.
$(function () {

    var $win = $(window);

    $win.scroll(function () {

        if ($win.height() + $win.scrollTop() == $(document).height()) {

            let cFiltro = ObtenerFiltro()

            VerMasProductos(cFiltro, iNumeroConsulta, "#contenedorSpiners");

        }
    });
});

$("#btnSalir").click(function () {
  
    CerrarSesion();

});


/**
 * FUNCIÓN PARA OBTENER EL FILTRO SELECCIONADO EN LA VISTA.
 * */
function ObtenerFiltro() {

    let cFiltro = "";

    $(".TipoFiltro").each(function () {//Etiquetas del filtro

        let cEstado = $(this).attr('Estado');//Estado de selección

        if (cEstado == '1') {

            cFiltro = $(this).attr('Filtro');//Valor de su filtro
        }
    })

    return cFiltro;
}

/**
 * FUNCIÓN PARA CAMBIAR EL MODO FILTRO A BÚSQUEDA EN LA VISTA.
 * */
function CambiarFiltroAbuscarProducto() {

    $(".TipoFiltro").each(function () {//Etiquetas del filtro

        let cFiltro = $(this).attr('Filtro');//Valor de su filtro

        if (cFiltro.toUpperCase() == "BUSCAR") $(this).attr('Estado', '1');

        else $(this).attr('Estado', '0');

    })

}






