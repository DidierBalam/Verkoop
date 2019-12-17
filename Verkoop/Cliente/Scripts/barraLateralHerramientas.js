
//Botón que redirigea a la vista de inicar sesión.
$("#btnAcceder").click(function () {

    window.location.href = "../Sesion/Index";

});

//BOTÓN PARA IR AL LISTADO DE LOS PRODUCTOS
$(document).on('click', '#btnProducto', function () {

    window.location.href = "/Cliente";

});

//BOTÓN PARA IR AL CARRITO DE COMPRAS
$(document).on('click', '#tbnCarrito', function () {

    window.location.href = "../CarritoCompras/CarritoCompras";

});

//BOTÓN PARA CERRAR SESIÓN
$(document).on('click', '#btnSalir',function () {

    window.location.href = "../Sesion/CerrarSesion";

});

//BOTÓN PARA IR AL HISTORIAL DE COMPRAS
$(document).on('click', '#btnCompras', function () {

    window.location.href = "../HistorialCompras/ComprasRealizadas";

});

//BOTÓN PARA IR AL PERFIL
$(document).on('click', '#btnPerfil', function () {
    window.location.href = "../Perfil/InformacionPersonal";
    
});
