var claimsGrid;

$(function () {
    loadClaimsGrid();
});

function loadClaimsGrid() {
    var claim = sessionStorage.getItem('selClaim');
    if (claim != null && user != undefined) {
        claim = JSON.parse(claim);
    }

    //define common ajax object for addition, update and delete.
    var ajaxObj = {
        dataType: "JSON",
        beforeSend: function () {
            this.showLoading();
        },
        complete: function () {
            this.hideLoading();
        },
        error: function () {
            this.rollback();
        }
    };

    //to check whether any row is currently being edited.
    function isEditing(grid) {
        var rows = grid.getRowsByClass({ cls: 'pq-row-edit' });
        if (rows.length > 0) {
            var rowIndx = rows[0].rowIndx;
            grid.goToPage({ rowIndx: rowIndx });
            //focus on editor if any 
            grid.editFirstCellInRow({ rowIndx: rowIndx });
            return true;
        }
        return false;
    }

    //called by add button in toolbar.
    function addRow(grid) {
        var rows = grid.getRowsByClass({ cls: 'pq-row-edit' });
        if (rows.length > 0) {//already a row currently being edited.
            var rowIndx = rows[0].rowIndx;

            //focus on editor if any 
            grid.editFirstCellInRow({ rowIndx: rowIndx });
        }
        else {
            let claim = sessionStorage.getItem('selClaim');
            if (claim != null && claim != undefined) {
                claim = JSON.parse(claim);
                if (claim != null && claim.ClientID > 0) {
                    //append empty row in the first row.                            
                    var rowData = {
                        claim: "", FirstName: "", LastName: "", Title: "", ClientID: claim.ClientID, Authentication: "ChangeYourPassword", StoreID: 0,
                        Department: 0, ReportsTo: 0, Email: "", Phone: "", Reset: true, Active: true, UserTypeID: 1, Error: "", AddressID: claim.AddressID
                    }; //empty row template
                    var rowIndx = grid.addRow({ rowIndxPage: 0, rowData: rowData, checkEditable: false });

                    //start editing the new row.
                    editRow(rowIndx, grid);
                }
            }
        }
    }

    //called by delete button.
    function deleteRow(rowIndx, grid) {
        grid.addClass({ rowIndx: rowIndx, cls: 'pq-row-delete' });

        var ans = window.confirm("Are you sure you want to delete this claim?");
        if (ans) {
            var claimId = grid.getRecId({ rowIndx: rowIndx });

            $.ajax($.extend({}, ajaxObj, {
                context: grid,
                type: 'DELETE',
                url: "/api/claim/" + claimID, //for ASP.NET, java
                headers: { "clientCode": clientCode },
                //data: '{ "ID": ' + claimID + '}',
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (response) {
                    this.refreshDataAndView(); //reload fresh page data from server.
                    response = JSON.parse(response);
                    if (response.data == null && response.message != null && response.message != '')
                        alert(response.message);
                    this.removeClass({ rowIndx: rowIndx, cls: 'pq-row-delete' });
                },
                error: function (response) {
                    response = JSON.parse(response);
                    this.removeClass({ rowIndx: rowIndx, cls: 'pq-row-delete' });
                    if (response.data == null && response.message != null && response.message != '')
                        alert(response.message);
                }
            }));
        }
        else {
            grid.removeClass({ rowIndx: rowIndx, cls: 'pq-row-delete' });
        }
    }
    //called by edit button.
    function editRow(rowIndx, grid) {

        grid.addClass({ rowIndx: rowIndx, cls: 'pq-row-edit' });
        grid.refreshRow({ rowIndx: rowIndx });

        grid.editFirstCellInRow({ rowIndx: rowIndx });
    }

    //called by update button.
    function update(rowIndx, grid) {

        if (grid.saveEditCell() == false) {
            return false;
        }

        if (!grid.isValid({ rowIndx: rowIndx, focusInvalid: true }).valid) {
            return false;
        }

        if (grid.isDirty()) {
            var ID = grid.getRecId({ rowIndx: rowIndx });
            var url,
                rowData = grid.getRowData({ rowIndx: rowIndx }),
                recIndx = grid.option("dataModel.recIndx"),
                type;

            grid.removeClass({ rowIndx: rowIndx, cls: 'pq-row-edit' });

            if (rowData[recIndx] == null) {
                console.log(rowData);
                //add record.
                type = 'add';
                url = "api/claim"; //ASP.NET, java
            }
            else {
                //update record.
                type = 'update';
                url = "api/claim/" + ID; //ASP.NET, java
            }
            $.ajax($.extend({}, ajaxObj, {
                context: grid,
                url: url,
                data: rowData,
                type: (type == 'add' ? 'POST' : 'PUT'),
                success: function (response) {
                    //response = JSON.parse(response);
                    if (type == 'add') {
                        rowData[recIndx] = response.ID;
                    }
                    //debugger;
                    grid.commit({ type: type, rows: [rowData] });
                    grid.refreshRow({ rowIndx: rowIndx });
                    if (response != null && response.Error != null && response.Error != '')
                        alert(response.Error);
                },
                error: function (response) {
                    if (response != null && response.Error != null && response.Error != '')
                        alert(response.Error);
                }
            }));
        }
        else {
            grid.quitEditMode();
            grid.removeClass({ rowIndx: rowIndx, cls: 'pq-row-edit' });
            grid.refreshRow({ rowIndx: rowIndx });
        }
    }

    //define the grid.
    var obj = {
        height: '600',
        width: '1200',
        rowHt: 25,
        wrap: false,
        hwrap: false,
        columnBorders: true,
        selectionModel: { type: 'row' },
        trackModel: { on: true }, //to turn on the track changes.            
        toolbar: {
            items: [
                {
                    type: 'button',
                    icon: 'ui-icon-plus',
                    label: 'Add C',
                    listener: function () {
                        addRow(this);
                    }
                }
            ]
        },
        scrollModel: { autoFit: true },
        validation: { icon: 'ui-icon-info' },
        title: "<b>Pending Claims</b>",
        colModel: [
            {
                title: 'ID', width: '50', dataType: 'integer', dataIndx: 'ID', hidden: true
            },
            {
                title: 'FIRST', width: '150', dataType: 'string', dataIndx: 'FirstName', hidden: false
            },
            {
                title: 'LAST', width: '150', dataType: 'string', dataIndx: 'LastName', hidden: false
            },
            {
                title: 'TITLE', width: '150', dataType: 'string', dataIndx: 'Title', hidden: false
            },
            {
                title: 'EMAIL', width: '300', dataType: 'string', dataIndx: 'Email', hidden: false
            },
            {
                title: 'PHONE', width: '150', dataType: 'string', dataIndx: 'Phone', hidden: false
            },
            {
                title: 'RESET PASSWORD', width: '175', dataType: 'bool', dataIndx: 'Reset', hidden: false,
                menuIcon: false,
                type: 'checkBoxSelection', cls: 'ui-state-default', sortable: false, editor: false,
                cb: {
                    all: false, //header checkbox to affect checkboxes on all pages.
                    header: false //for header checkbox.
                }
            },
            {
                title: "ACTIVE", width: 100, dataIndx: "Active",
                menuIcon: false,
                type: 'checkBoxSelection', cls: 'ui-state-default', sortable: false, editor: false,
                dataType: 'bool',
                cb: {
                    all: false, //header checkbox to affect checkboxes on all pages.
                    header: false //for header checkbox.
                }
            },
            {
                title: "", editable: false, minWidth: 110, sortable: false,
                render: function (ui) {
                    return "<button type='button' class='edit_btn'></button><button type='button' class='delete_btn'></button>";
                },
                postRender: function (ui) {
                    var rowIndx = ui.rowIndx,
                        grid = this,
                        $cell = grid.getCell(ui);

                    if (grid.hasClass({ rowData: ui.rowData, cls: 'pq-row-edit' })) {

                        //update button
                        $cell.find(".edit_btn")
                            .button({ label: "", icons: { primary: 'ui-icon-check' } })
                            .off("click")
                            .on("click", function () {
                                return update(rowIndx, grid);
                            });

                        //cancel button
                        $cell.find(".delete_btn")
                            .button({ label: "", icons: { primary: 'ui-icon-cancel' } })
                            .off("click")
                            .on("click", function () {
                                grid.quitEditMode();
                                grid.removeClass({ rowIndx: rowIndx, cls: 'pq-row-edit' })
                                grid.rollback();
                            });
                    }
                    else {

                        //edit button
                        $cell.find(".edit_btn").button({ icons: { primary: 'ui-icon-pencil' } })
                            .off("click")
                            .on("click", function (evt) {
                                if (isEditing(grid)) {
                                    return false;
                                }
                                editRow(rowIndx, grid);
                            });

                        //delete button
                        $cell.find(".delete_btn").button({ icons: { primary: 'ui-icon-trash' } })
                            .off("click")
                            .on("click", function (evt) {
                                deleteRow(rowIndx, grid);
                            });
                    }
                }
            }
        ],
        postRenderInterval: -1,
        dataModel: {
            dataType: "JSON",
            location: "remote",
            recIndx: "ID",
            url: "/api/claim?clientId=" + claim.ClientID,
            headers: { "clientCode": clientCode },
            getData: function (response) {
                response = JSON.parse(response);
                if (response.data == null && response.message != null && response.message != '') {
                    alert(response.message);
                }
                return { data: response.data, curPage: response.curPage, totalRecords: response.totalRecords };
            }
        },

        //make rows editable based upon the class.
        editable: function (ui) {
            return this.hasClass({ rowIndx: ui.rowIndx, cls: 'pq-row-edit' });
        },
        create: function (evt, ui) {
            this.widget().pqTooltip();
        }
    }

    if (claimGrid == null)
        claimGrid = pq.grid("#divDobsonClaims", obj);
    else {
        claimGrid.options.dataModel.url = "/api/claim?clientID=" + claim.ClientID;
        claimGrid.refreshDataAndView();
    }

    //var $grid = $("#divDobsonClaims").pqGrid(obj);
    //$grid.pqGrid('option', 'bottomVisible', true);
    ////$grid.refresh();
    $('#divWait').hide();
}
