



$(document).ready(function () {
    BarraHerramientas();
    ObtenerProductos();

    $('#tblProductos tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
            iIdProducto = 0;
        }
        else {
            Datatables.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
            iIdProducto = $(this).attr('data-iIdProducto');
        }
    });
});

$(function () {
    $('#toggle-event').change(function () {
        iEstatus = $(this).prop('checked');
        ObtenerProductos();
    })
})

function ObtenerProductos() {

    Datatables = $('#tblProductos').DataTable({
        destroy: true,
        ajax: {
            url: "../Producto/ObtenerProductos?iEstatus=" + iEstatus ,
            type: "POST",
            dataType: "JSON",
            dataSrc: "data"
        },
        createdRow: function (row, data) {
            $(row).attr('data-iIdProducto', data.iIdProducto);
        },
        columns: [
            { title: "ID", data: "iIdProducto" },
            { title: "Nombre", data: "cNombre" },
            { title: "Cantidad", data: "iCantidad" },
            { title: "Precio", data: "dPrecio" },
            {
                title: "Fecha Alta",
                data: "dtFechaAlta",
                render: function (jsonDate) {
                    var newdate = new Date(parseInt(jsonDate.substr(6)));
                    return newdate.format("mm/dd/yyyy");
                }
            },
            {
                title: "Fecha Modificacion",
                data: "dtFechaModificacion",
                render: function (jsonDate) {
                    if (jsonDate) {
                        var newdate = new Date(parseInt(jsonDate.substr(6)));
                        return newdate.format("mm/dd/yyyy");
                    } else {
                        return jsonDate;

                    }
                }
            }
        ],
        language: {
            "sProcessing": "Procesando...",
            "sLengthMenu": "Mostrar _MENU_ registros",
            "sZeroRecords": "No se encontraron resultados",
            "sEmptyTable": "Ningún dato disponible en esta tabla",
            "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
            "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
            "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
            "sInfoPostFix": "",
            "sSearch": "Buscar:",
            "sUrl": "",
            "sInfoThousands": ",",
            "sLoadingRecords": "Cargando...",
            "oPaginate": {
                "sFirst": "Primero",
                "sLast": "Último",
                "sNext": "Siguiente",
                "sPrevious": "Anterior"
            }
        },
        "columnDefs": [{
            "targets": 0,
            "visible": false
        }]
    });

    
}

function BarraHerramientas() {
    $("#btnVisualizarProduco").click(function (e) {
        e.preventDefault();

        if (iIdProducto) {
            ObtenarVista("POST", "../Producto/Visualizar", { iIdProducto: iIdProducto });
        } else {
            Swal.fire({
                title: 'Oops...',
                text: Msn,
                icon: 'warning',
                customClass: {
                    popup: 'animated tada'
                }
            })
        }
    });
    $("#btnEliminarProducto").click(function (e) {
        e.preventDefault();
        if (iIdProducto) {
            DeshabilitarProducto(iIdProducto, iEstatus);
        } else {
            Swal.fire({
                title: 'Oops...',
                text: Msn,
                icon: 'warning',
                showClass: {
                    popup: 'animated tada faster'
                },
                hideClass: {
                    popup: 'animated slideOutUp faster'
                }
            })
        }
    });
}