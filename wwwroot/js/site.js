let pathVisit = '/api/VisitApi';
let pathRecover = '/api/RecoverApi'
let parLocker = '/api/LockerApi'
$('#exampleModal').on('show.bs.modal', function (event) {
    var button = $(event.relatedTarget); 
    var animalId = button.data('animal-id'); 
    var modal = $(this);
    var buttonVisit = $('#buttonVisit') 

    $.ajax({
        url: `${pathVisit}/${animalId}`,
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

$("#researchM").on('click', () => {
    let microchip = $("#microchipValue").val();

    $.ajax({
        url: `${pathRecover}/${microchip}`,
        method: 'GET',
        success: (data) => {
            console.log(data)
            let dataDiv = $("#information")
            dataDiv.empty();
            dataDiv.append(`
            <img src="${data.image}">
            <p>${data.animal.name}<p>
            <p>${data.animal.dateRegister}<p>
            <p>${data.animal.type}<p>
            <p>${data.dateRecover}<p>
            <p>${data.isActive}<p>
            `)
        }
    })
})

