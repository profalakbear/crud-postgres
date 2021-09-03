$(document).ready(function () {
    var url = "/Company/GetCompanies/";
    $.get(url, null, function (data) {
        var array = data.data
        if (array) {
            array.forEach(function (elem) {
                var html = `
                <tr class="user_member" data-id="${elem.Id}">
                    <td>${elem.Name}</td>
                    <td>${elem.Phone}</td>
                    <td>${elem.TaxNo}</td>
                    <td>${elem.Address}</td>
                    <td>
                        <button type="button" class="see btn btn-primary" data-toggle="modal" data-id="${elem.Id}" data-target="#seeModal"><i class="fas fa-eye"></i></button>
                        <button type="button" class="update btn btn-warning" data-toggle="modal" data-id="${elem.Id}" data-target="#updateModal"><i class="fas fa-pen"></i></button>
                        <button type="button" class="delete btn btn-danger" data-id="${elem.Id}"><i class="far fa-trash-alt"></i></button>
                    </td>
                </tr>
`
                $('#tableBody').append(html)
            }, this);
        }
    })
});

$(document).on('click', '.see', function () {
    var url = "/Company/PopulateModal/";
    var id = $(this).attr("data-id");
    $.get(url, { id: id }, function (data) {
        var myhtml = `

                        <div>
                            <div>
                                <span>
                                    <b>Id:</b>
                                </span>

                                <span>
                                    ${data.data.Id}
                                </span>
                            </div >

                            <div>
                                <span><b>Name: </b></span>
                                <span>${data.data.Name}</span>
                            </div>

                            <div>
                                <span><b>Phone: </b></span>
                                <span>${data.data.Phone}</span>
                            </div>

                            <div>
                                <span><b>TaxNo: </b></span>
                                <span>${data.data.TaxNo}</span>
                            </div>

                            <div>
                                <span><b>Address: </b></span>
                                <span>${data.data.Address}</span>
                            </div>

                        </div >`

        $("#seeModalInner").html(myhtml);
    });
});

$(document).on('click', '.delete', function () {
    var url = "/Company/Delete/";
    var id = $(this).attr("data-id");
    var remove_item = $(this).closest(".user_member");
    $.get(url, { id: id }, function (data) {
        if (data.data) {
            remove_item.fadeOut(300, function () {
                remove_item.remove();
            })
        }
    })
});

$(document).on('click', '#UpdateCompany', function () {
    var url = "/Company/Update/";
    var id = $(this).attr("data-id");
    var formData = {
        Id: $("#updateId").val(),
        Name: $("#inputNameUpdate").val(),
        Phone: $("#inputPhoneUpdate").val(),
        TaxNo: $("#inputTaxUpdate").val(),
        Address: $("#inputAddressUpdate").val()
    };


    if (formData) {
        $.ajax({
            type: "POST",
            url: "/Company/Update/",
            data: JSON.stringify(formData),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            timeout: 5000,
            success: function (data) {
                if (data.data) {
                    var company = data.data;
                    $("#inputNameUpdate").val(company.Name),
                    $("#inputPhoneUpdate").val(company.Phone),
                    $("#inputTaxUpdate").val(company.TaxNo),
                    $("#inputAddressUpdate").val(company.Address)

                    var html = `
                        <td>${company.Name}</td>
                        <td>${company.Phone}</td>
                        <td>${company.TaxNo}</td>
                        <td>${company.Address}</td>
                        <td>
                            <button type="button" class="see btn btn-primary" data-toggle="modal" data-id="${company.Id}" data-target="#seeModal"><i class="fas fa-eye"></i></button>
                            <button type="button" class="update btn btn-warning" data-toggle="modal" data-id="${company.Id}" data-target="#updateModal"><i class="fas fa-pen"></i></button>
                            <button type="button" class="delete btn btn-danger" data-id="${company.Id}"><i class="far fa-trash-alt"></i></button>
                        </td>
                    `
                    $("#tableBody").find("tr").last().html('');
                    $("#tableBody").find("tr").last().html(html);

                }
            }
        })
    }
});

$(document).ready(function () {
    $("#addNewUser").click(function (event) {
        var formData = {
            Name: $("#inputName").val(),
            Phone: $("#inputPhone").val(),
            TaxNo: $("#inputTax").val(),
            Address: $("#inputAddress").val()
        };

        if (formData) {
            $.ajax({
                type: "POST",
                url: "/Company/Create/",
                data: JSON.stringify(formData),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                timeout: 5000,
                success: function (data) {
                    if (data.data) {
                        var company = data.data;
                        var html = `
                        <tr class="user_member" data-id=${company.Id}">
                            <td>${company.Name}</td>
                            <td>${company.Phone}</td>
                            <td>${company.TaxNo}</td>
                            <td>${company.Address}</td>
                            <td>
                                <button type="button" class="see btn btn-primary" data-toggle="modal" data-id="${company.Id}" data-target="#seeModal"><i class="fas fa-eye"></i></button>
                                <button type="button" class="update btn btn-warning" data-toggle="modal" data-id="${company.Id}" data-target="#updateModal"><i class="fas fa-pen"></i></button>
                                <button type="button" class="delete btn btn-danger" data-id="${company.Id}"><i class="far fa-trash-alt"></i></button>
                            </td>
                    </tr>
                    `
                        $('#tableBody').append(html);
                    }
                }
            })
        }

        event.preventDefault();
        $("#inputName").val('');
        $("#inputPhone").val('');
        $('#inputTax').val('');
        $('#inputAddress').val('');

    })
});


$(document).on('click', '.update', function () {
    var url = "/Company/PopulateModal/";
    var id = $(this).attr("data-id");
    $.get(url, { id: id }, function (data) {
        $("#updateId").val(data.data.Id);
        $("#inputNameUpdate").val(data.data.Name);
        $("#inputPhoneUpdate").val(data.data.Phone);
        $('#inputTaxUpdate').val(data.data.TaxNo);
        $('#inputAddressUpdate').val(data.data.Address);
    })
});