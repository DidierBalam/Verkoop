let iIdUsuario;

$(document).ready(function () {
    BarraHerramientas();
    ObtenerClientes();

});

function ObtenerClientes() {

    Datatables = $('#tblClientes').DataTable({
        ajax: {
            url: "../Cliente/ObtenerClientes",
            type: "POST",
            dataType: "JSON",
            dataSrc: "data"
        },
        createdRow: function (row, data) {
            $(row).attr('data-iIdUsuario', data.iIdUsuario);
        },
        columns: [
            { title: "ID", data: "iIdUsuario" },
            { title: "Nombre", data: "cNombre" },
            { title: "Teléfono", data: "cTelefono" },
            {
                title: "Fecha Alta",
                data: "dtFechaIngreso",
                render: function (jsonDate) {
                    var newdate = new Date(parseInt(jsonDate.substr(6)));
                    return newdate.format("mm/dd/yyyy");
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

    $('#tblClientes tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
            iIdUsuario = 0;
        }
        else {
            Datatables.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
            iIdUsuario = $(this).attr('data-iIdUsuario');
        }
    });
}

function BarraHerramientas() {
    $("#btnVisualizarCliente").click(function (e) {
        e.preventDefault();

        if (iIdUsuario) {
            ObtenarVista("POST", "../Cliente/Visualizar", { iIdUsuario: iIdUsuario });
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
}