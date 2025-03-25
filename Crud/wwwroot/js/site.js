// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let table = new DataTable('#myTable', {
    language: {
        emptyTable: "Nenhum registro encontrado na tabela",
        info: "Mostrar _START_ até _END_ de _TOTAL_ registros",
        infoEmpty: "Mostrar 0 até 0 de 0 registros",
        infoFiltered: "(Filtrar de _MAX_ total de registros)",
        thousands: ".",
        lengthMenu: "Mostrar _MENU_ registros por página",
        loadingRecords: "Carregando...",
        processing: "Processando...",
        zeroRecords: "Nenhum registro encontrado",
        search: "Pesquisar:",
        paginate: {
            next: "Próximo",
            previous: "Anterior",
            first: "Primeiro",
            last: "Último"
        },
        aria: {
            sortAscending: ": Ordenar colunas de forma ascendente",
            sortDescending: ": Ordenar colunas de forma descendente"
        }
    }
});

