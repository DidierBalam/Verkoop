$(document).ready(function () {
    Boton();

});


function Boton() {

    $(".btnCarrito").click(function (e) {

        e.preventDefault();

        let iIdProducto = $(this).attr('IdProducto');

        console.log(iIdProducto);
        //ObtenerMetodoControlador("POST", "../CarritoCompras/AgregarProductoCarrito", { IdProducto = iIdProducto });

    });

    $(".btnEliminar").click(function (e) {

        e.preventDefault();

        let iIdProducto = $(this).attr('IdProducto');

        console.log(iIdProducto);
        //ObtenerMetodoControlador("POST", "../CarritoCompras/QuitarProductoCarrito", { IdProducto = iIdProducto });

    });

}

