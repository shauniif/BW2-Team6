let pathVisit = '/api/VisitApi';
let pathRecover = '/api/RecoverApi';
let pathLocker = '/api/LockerApi';
let pathResearch = '/api/ResearchApi'

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

$("#ResearchFC").on('click', () => {
    let fiscalCode = $("#fiscalCode").val()
    $.ajax({
        url: `${pathResearch}/${fiscalCode}`,
        method: 'GET',
        success: (data) => {
            console.log(data)
            let fiscalCodeInformation = $("#fiscalCodeInformation");
            fiscalCodeInformation.empty();
            $(data).each((_, inf) => {
                fiscalCodeInformation.append(`
                    <p>Nome: ${inf.owner.firstName} ${inf.owner.lastName}</p>
                    <p>Prodotto Acquistato: ${inf.product.name}</p>
                    <p>Data di vendita: ${inf.dateSell}</p>
                `);
            })
        }
    })
})

$("#ResearchD").on('click', () => {
    let date = $("#date").val()

    $.ajax({
        url: `${pathResearch}/SellByDate/${date}`,
        method: 'GET',
        success: (data) => {
            console.log(data)
            let dateInformation = $("#dateInformation");
            dateInformation.empty();
            $(data).each((_, inf) => {
                dateInformation.append(`
                                <p>Data di vendita: ${inf.dateSell}</p>
                                <p>Prodotto Acquistato: ${inf.product.name}</p>
                            `);
;
            })
        }
    })
})
$(document).on('click','.ReasearchP', function() {
    let product = $(this).data('product-id')
    console.log(product)
    $.ajax({
        url: `${pathResearch}/SearchProduct/${product}`,
        method: 'GET',
        success: (data) => {
            console.log(data)
            let productInf = $("#productInf")

            productInf.empty();
            let drawerInfo;
            if (data.drawer.length == 0) {

                drawerInfo = `non si trova da nessuna parte.`
            } else {
                drawerInfo = data.drawer.map(draw =>
                `cassetto n°: ${draw.drawer.id} nel armadietto numero ${draw.drawer.locker.numberLocker}`
            ).join(', ');
            }
            // Costruisci la stringa del contenuto

            let content = `
                Il prodotto ${data.name} si trova in ${drawerInfo}
            `;

            // Aggiungi il contenuto al div
            productInf.html(content);
        }
    })
})