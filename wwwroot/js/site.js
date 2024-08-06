let path = '/api/VisitApi';

$('#exampleModal').on('show.bs.modal', function (event) {
    var button = $(event.relatedTarget); 
    var animalId = button.data('animal-id'); 
    var modal = $(this);
    var buttonVisit = $('#buttonVisit') 

    $.ajax({
        url: `${path
            }/${animalId}`,
        type: 'GET',
        success: (data) => {
            var visitsTableBody = modal.find('#visitsTableBody');
            visitsTableBody.empty(); 


            $(data).each((_, visit) => {
                visitsTableBody.append(
                    '<tr>' +
                    '<td>' + visit.dateVisit + '</td>' +
                    '<td>' + visit.typeOfExam + '</td>' +
                    '<td>' + visit.typeOfCure + '</td>' +
                    '</tr>'
                );
            });
            buttonVisit.append(`<a class="btn btn-primary" href=/Visit/Create/${animalId}> Registra una nuova visita </a>`)
        },

        error: function () {
            alert('Errore nel recupero delle visite.');
        }
    });
});