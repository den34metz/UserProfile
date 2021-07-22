

var getTrainData = null;
var trainData = [];
var curEdipi = 0;
var curPersonnel = "";
var pocs = null;
var schedDates;
var schedData;
var userdates = null;
var userTrainingArray = [];
var fileByteArray = [];
var freqArray = [];
var allSchedDates = [];

$(document).ready(function(){
	getCurEdipi();
	//getAllSchedDates(); 
	getUserDates();

	// Event when user clicks Column/Row in the Training Datatable
	$('#trainingTable').on('click', 'tbody td', function () {
		event.preventDefault();
		var schedArray = [];	
		var schedId;
		var schedTrainId;
		var schedDates = [];
		var table = $('#trainingTable').DataTable();
	
		// Get clicked table row training details
		if (table.cell(this).index().columnVisible == 2) {
			setRowData = (table.row(this).data());

			var trainId = setRowData.TRAINID;
			var trainTitle = setRowData.TRAIN_TITLE;
			var trainDetails = setRowData.TRAIN_DETAILS;
			var url = setRowData.URL;
			var freq = setRowData.FREQUENCY;
			var dur = setRowData.CLASS_DUR;
			var atcts = setRowData.ATCTS;
			var type = setRowData.TYPE;
			var curSchedDate = setRowData.scheddate;
			var tloc = null;
			var nLoc = "N/A";
			
			//get sched dates data for location field in modal
			var singleSchedDate = getSingleSchedDates();
			var singleSdate = singleSchedDate.responseJSON.data;
			for (var i = 0; i < singleSdate.length; i++) {
				var loc = singleSdate[i].LOC;
				var locTrainId = singleSdate[i].TRAIN_ID;
				var schedEdipi = singleSdate[i].EDIPI;
				var schedDate = singleSdate[i].DATESCHED;
				schedDate = schedDate.substr(6);
						schedDate = new Date(parseInt(schedDate));
						schedDate = formatDateTime(schedDate);

				if (trainId == locTrainId && schedDate == curSchedDate) {
					tloc = loc;
				} else if(tloc == null || tloc == "N/A"){
					tloc = nLoc;
				}

			}

			//Set training details modal fields
			$("#txtTrainTitle").val(trainTitle);
			$("#txtFreq").val(freq);
			$("#txtDur").val(dur +" "+ "min");
			$("#urlInput").val(url);
			$("#txtarea2").val(trainDetails);
			$("#txtType").val(type);
			$("#txtTrainLoc").val(tloc);

			//set radio buttons(atcts)
			if (atcts == "Yes") {
				$("#group1").prop("checked", true);
				$("#faExclam").show();
			} else {
				$("#group2").prop("checked", true);
				$("#faExclam").hide();
			}
			
			$('#addTrainingModal').modal('show');
		}

		// Make the DateTaken column only clickable to display modal
		if (table.cell(this).index().columnVisible == 3) {
			setDate = (table.row(this).data());

			var id = setDate.TRAINID;
			var traintitle = setDate.TRAIN_TITLE;
			var edipi = setDate.edipi;

			// post data to modal fields
			$("#txtTrainId").val(id);
			$("#txtUpdateTrainTitle").val(traintitle);
			$("#txtEdipi").val(curEdipi);

			var tr = $('td', this).eq(1).text();
			if (this.innerText == "Add") {
				//$('#dateTimeModal').modal('@ViewBag.ModalState');

				$.get("/Home/DateCertModal", function () {
					$('#addTrainingModalContent').html();
					$('#dateTimeModal').modal('show');

				});

			} else {
				setRowData = (table.row(this).data());
				var trainId = setRowData.TRAINID;
				var edipi = setRowData.edipi;
				var trainTitle = setRowData.TRAIN_TITLE;
				var dateTaken = setRowData.dateTaken;
				//var files = setRowData.file;

				//Set modal fields with data
				$('#txtTrainId').val(trainId);
				$('#txtEdipi').val(edipi);
				$('#updateTrainTitle').val(trainTitle);

				$.get("/Home/DateCertModal", function () {
					$('#updateTrainingModalContent').html();
					$('#updateDateCertModal').modal('show');

				});
			}

		}

		// Make the Scheduled column only
		// clickable to display modal
		if (table.cell(this).index().columnVisible == 6) {
			var newSchedDate = [];

			setRowData = (table.row(this).data());
			if (setRowData.TYPE != "Online" && this.innerText == "Schedule") {
				var userDataArray = data.responseText.toString();
				var scheduleddate = JSON.parse(userDataArray);
				var schedTitle = setRowData.TRAIN_TITLE;
				var schedId = setRowData.TRAINID;

				scheduleddate = scheduleddate.data;

				newSchedDate = removeDup(schedData, "TRAIN_DATE_TIME");

				for (var s = 0; s < newSchedDate.length; s++) {

					var allData = newSchedDate[s];
					var newSchedTitle = newSchedDate[s].TRAIN_TITLE;
					var datetimeID = newSchedDate[s].DATE_TIME_ID;
					var location = newSchedDate[s].LOC;

					if (newSchedTitle == schedTitle) {
						schedId = datetimeID;
						var id = setRowData.TRAINID;
						var traintitle = setRowData.TRAIN_TITLE;

						var edipi = curEdipi;
						var dates = newSchedDate[s].TRAIN_DATE_TIME;
							dates = dates.substr(6);
						var date = new Date(parseInt(dates));
						var curDate = new Date();
						//Check if current Date is < schedule date
						if (curDate < date) {
							// Create date format to display in scheduled modal
							var thisDate = ("0" + (date.getMonth() + 1)).slice(-2) + '/' + ("0" + (date.getDate() + 1)).slice(-2) + '/' + date.getFullYear() + " " + ("0" + (date.getHours())).slice(-2) + ":" + ("0" + (date.getMinutes())).slice(-2);
							$('#selectSched').append('<option>' + thisDate + '</option>');
						}

						//post data to modal fields
						$("#txtSchedTrainId").val(id);
						$("#txtSchedTrainTitle").val(traintitle);
						$("#txtDateTakenId").append(schedId);
						$("#txtSchedEdipi").val(edipi);
						$("#txtDateTimeId").val(datetimeID);
					

						console.log("Edipi works");
					} else {
						schedId = 0;
					}
					// }
				}
				if (schedTitle != undefined) {
					$("#txtSchedTrainId").val(setRowData.TRAINID);
					$("#txtSchedTrainTitle").val(schedTitle);
					$("#txtSchedEdipi").val(curEdipi);
					$("#txtDateTimeId").val(datetimeID);
					$('#schedDatesModal').modal("show");
					$('#schedDatesModal').on('hidden.bs.modal', function () {
						removeOptions(document.getElementById('selectSched'));
						$('#selectSched').append('<option>' + "Select Date" + '</option>');

					})
				} else {
					$('#noSchedDatesModal').modal("show");

				}

			}
			rowData = setRowData;
		}
		// Event when user clicks a Training
		// Cert Column in the Training Datatable
		if (table.cell(this).index().columnVisible == 7) {
			getPDF = (table.row(this).data());

			/*$("div.spanner").addClass("show");
			$("div.overlay").addClass("show");*/

			var edipi = getPDF.edipi;
			var trainid = getPDF.TRAINID;
			var datetakenid = getPDF.datetakenid;

			data = $.ajax({
				"url": "/UserDates/GetCert",
				"type": "POST",
				"dataType": "json",
				"data": {
					'datetakenid': datetakenid
				},
				"success": function (data) {
					/*$("div.spanner").removeClass("show");
					$("div.overlay").removeClass("show");*/
					data = data.data;
					for (var i = 0; i < data.length; i++) {
						var cert = data[i].CERT;
						var name = data[i].CERTNAME;
						var certdatetakenid = data[i].DATETAKENID;

						if (datetakenid == certdatetakenid) {
							/*function base64ToArrayBuffer(base64) {
								for (var i = 0; i < cert; i++) {
									var ascii = binaryString.charCodeAt(i);
									bytes[i] = ascii;
								}
								return bytes;
							}*/

							/*	var sampleBytes = base64ToArrayBuffer(cert);*/
							cert = new Uint8Array(cert);
							var saveByteArray = (function () {

								var a = document.createElement("a");
								document.body.appendChild(a);

								return function (cert, name) {
									var blob = new Blob(cert, {
										type: "octet/stream"
									}), url = URL.createObjectURL(blob);
									if (navigator.appVersion.toString().indexOf('.NET') > 0) {
										window.navigator.msSaveOrOpenBlob(blob, name);
									} else

										if (navigator.msSaveBlob) {
											navigator.msSaveBlob(csvData, exportFilename);
										} else {
											a.href = url;
											a.download = name;
											// window.open(blob);
											a.click();
											document.body.removeChild(a);
											window.URL.revokeObjectURL(url);
										}

								};

							}());
							saveByteArray([cert], name);

						}
					}

				},
				"error": function (error) {
					alert("error: " + error.responseText + ", " + error.StatusText);
				}

			})// end ajax call

		}// end if statement

	});

	$(".custom-file-input").on("change", function () {
		var fileName = $(this).val().split("\\").pop();
		$(this).siblings(".custom-file-label").addClass("selected").html(fileName);
	});

	// Clear modal content on close
	$('#btnClose').on('click', function () {
		$(".modal-body input").val("");
	});
	
	// Clear modal content on close
	$('#btClose').on('click', function (e) {
		$(".modal-body input").val("");
		$("#dateTimeModal").modal('hide');
	});

	//Event when user clicks on exclamation icon
	$("#faExclam").on('click', function () {
		$('.modal-body').load('content.html', function () {
			$('#myModal').modal({ show: true });
		})
	});

});//End Doc Ready



/**
   * This method uses java.io.FileInputStream to read
   * file content into a byte array
   * @param file
   * @return
   */

function removeOptions(selectElement) {
	var i, L = selectElement.options.length - 1;
	for (i = L; i >= 0; i--) {
		selectElement.remove(i);
	}
}

//Save DateTaken column data
function saveModalData(TYPE) {
	var cert;
	var files;

	if (TYPE == "Update") {
		cert = $('#inputGroupFile02').get(0);
		files = cert.files;
	} else {
		cert = $('#inputGroupFile01').get(0);
		files = cert.files;
	}
	
	var TRAIN_ID = $('#txtTrainId').val();
	var EDIPI = $('#txtEdipi').val();
	var TRAINTITLE = $('#txtUpdateTrainTitle').val();
	
	var DATETAKEN;
	if (TYPE == "Update") {
		DATETAKEN = $('#classDate').val();
		DATETAKEN = new Date(DATETAKEN);
		DATETAKEN = DATETAKEN.toLocaleDateString();
	} else {
		DATETAKEN = $('#clasDate').val();
		DATETAKEN = new Date(DATETAKEN);
		DATETAKEN = DATETAKEN.toLocaleDateString();
	}
	
	
	var formData = new FormData();
	var DATENEXTDUE = null;

	for (var i = 0; i < files.length; i++) {
		console.log('(files[i].name' + files[i].className);
		formData.append('file', files[i]);
	}
	if (DATETAKEN != null) {
		DATENEXTDUE = new Date(DATETAKEN);
		DATENEXTDUE = DATENEXTDUE.toLocaleDateString();
	}
	//ADD DATA to formData
	formData.append('TRAIN_ID', TRAIN_ID);
	formData.append('EDIPI', EDIPI);
	formData.append('DATETAKEN', DATETAKEN);
	formData.append('DATENEXTDUE', DATENEXTDUE);
	formData.append('TRAINTITLE', TRAINTITLE);
	formData.append('TYPE', TYPE);

	
	var userDateObj = {
		TRAIN_ID: $('#txtTrainId').val(),
		EDIPI: $('#txtEdipi').val(),
		DATETAKEN: $('#classDate').val(),
		CERT: formData
	};

	$.ajax({
		type: "POST",
		url: "/UserDates/InsertUserDates",
		data: formData,
		cache: false,
		enctype: "multipart/form-data",
		processData: false,
		contentType: false,
		success: function () {
			alert("Insert worked!");
		},

		error: function (error) {
			alert("error: " + error.responseText + ", " + error.statusText);
		}
	});
}
function getAllSchedDates() {
	data = $.ajax({
		"url": "/UserDates/GetSchedDates",
		"type": "GET",
		"dataSrc": "",
		"async": false,
		"success": function (data) {
			var parsedSched = data.data;
			schedData = parsedSched;
			schedDates = parsedSched;
		},
		"error": function (error) {
			alert("error: " + error.responseText + ", " + error.StatusText);
		}
	})

}

function saveSchedDate() {
	var TRAINDATETIME = $('#selectSched').val();
	var EDIPI = $('#txtSchedEdipi').val();
	var TRAINID = $('#txtSchedTrainId').val();
	var TRAINTITLE = $('#txtSchedTrainTitle').val();
	var DATETIMEID = $('#txtDateTimeId').val();
	var CURDATE = new Date();
	CURDATE = CURDATE.toLocaleDateString();

	var formData = new FormData();
	formData.append('TRAIN_DATE_TIME', TRAINDATETIME);
	formData.append('EDIPI', EDIPI);
	formData.append('TRAIN_ID', TRAINID);
	formData.append('TRAIN_TITLE', TRAINTITLE)
	formData.append('DATE_TIME_ID', DATETIMEID);
	formData.append('DATEMODIFIED', CURDATE);

	$.ajax({
		type: "POST",
		url: "/UserDates/InsertSchedDates",
		data: formData,
		cache: false,
		enctype: "multipart/form-data",
		processData: false,
		contentType: false,
		success: function () {
			alert("Insert worked!");
		},

		error: function (error) {
			alert("error: " + error.responseText + ", " + error.statusText);
		}
	});
}

function getPocs() {
	data = $.ajax({
		"url": "/Home/GetPoc",
		"type": "POST",
		"data": {
			'curEdipi': curEdipi
		},
		"success": function (data) {
			pocs = data.data;
			gettrainData = getTrainings();
			

			//trainingTable(trainData);
		},
		"error": function (error) {
			alert("error: " + error.responseText + ", " + error.StatusText);
		}
	});
}

function getCurEdipi(){
	data = $.ajax({
		"url": "/Home/GetCurrEdipi",
		"type": "GET",
		"dataType": "json",
		"async": false,
		"success": function (data) {
			curEdipi = data.data;
			/*getUserDates();
				getPocs();*/
		},
		"error": function (error) {
			alert("error: " + error.responseText + ", " + error.StatusText);
		}
	});
}
//get data to display in the trainings datatable
function getTrainings() {
	var curTrainings = [];
	var getSchedDate = getSingleSchedDates();
	getSchedDate = getSchedDate.responseJSON.data
	data = $.ajax({
		"url": "/Home/GetTrainingData",
		"type": "GET",
		"dataType": "json",
		"success": function (data) {
			var curPocReq = pocs[0].RT_DESCRIPTION;
			trainData = data.data;
			freqArray = trainData;
			for (var i = 0; i < trainData.length; i++) {
				var curTrainData = trainData[i];
				var req = trainData[i].REQ_NAME;
				var tid = trainData[i].TRAINID;
				var freq = trainData[i].FREQUENCY;

				if (req == curPocReq) {
					for (var s = 0; s < getSchedDate.length; s++) {

						var schedEdipi = getSchedDate[s].EDIPI;
						var schedTrainId = getSchedDate[s].TRAIN_ID;
						var loc = getSchedDate[s].LOC;
						var schedDate = getSchedDate[s].DATESCHED;
						schedDate = schedDate.substr(6);
						schedDate = new Date(parseInt(schedDate));
						schedDate = formatDateTime(schedDate);

						if (schedTrainId == tid && schedEdipi == curEdipi) {
							trainData[i].scheddate = schedDate;
							trainData[i].loc = loc;
						}
					}
					
					var curUserId = parseInt(curEdipi);

					for (var d = 0; d < userdates.length; d++) {
						var allDatesData = userdates[d];
						var datetaken = userdates[d].DATETAKEN;
						datetaken = datetaken.substr(6);
						datetaken = new Date(parseInt(datetaken));
						datetaken = formatDate(datetaken);
						var datenextdue = userdates[d].DATENEXTDUE;
						var datetakenid = userdates[d].DATETAKENID;
						var trainid = userdates[d].TRAIN_ID;
						var edipi = userdates[d].EDIPI;
						var verified = userdates[d].VERIFIED;

						if (curEdipi == edipi && tid == trainid) {
							
							for (var f = 0; f < freqArray.length; f++) {
								var freqTrainid = freqArray[f].TRAINID;
								var curfreq = freqArray[f].FREQUENCY;

								if (freqTrainid == tid) {
									switch (curfreq) {
										case "Once":
											trainData[i].datenextdue = "N/A";
											break;
										case "Annual":
											var newdate = new Date(datetaken);
											var formatted_date = (newdate.getFullYear() + 1) + "-" + ('0' + (newdate.getMonth() + 1)).slice(-2) + "-" + ('0' + newdate.getDate()).slice(-2)
											trainData[i].datenextdue = formatted_date;
											break;
										case "Every two years":
											var newdate = new Date(datetaken);
											var formatted_date = (newdate.getFullYear() + 2) + "-" + ('0' + (newdate.getMonth() + 1)).slice(-2) + "-" + ('0' + newdate.getDate()).slice(-2)
											trainData[i].datenextdue = formatted_date;
											break;
										case "Every three years":
											var newdate = new Date(datetaken);
											var formatted_date = (newdate.getFullYear() + 3) + "-" + ('0' + (newdate.getMonth() + 1)).slice(-2) + "-" + ('0' + newdate.getDate()).slice(-2)
											trainData[i].datenextdue= formatted_date;
											break;
										default:
											trainData[i].datenextdue = "N/A";
											break;

									}
								}
							}

							trainData[i].edipi = curEdipi;
							trainData[i].dateTaken = datetaken;
							trainData[i].verified = verified;
							trainData[i].FREQUENCY = freq;
							trainData[i].datetakenid = datetakenid;
							
							//curTrainings.push(curTrainData);
						} else {
							trainData[i].edipi = edipi;
						}

					}

					curTrainings.push(curTrainData);
				}
				// adds new single training if rank type = "OTHER"
				if (req == "OTHER") {
					var singleT = getSingleTraining();
					var singleTrain = singleT.responseText.toString();
					var sTresult = JSON.parse(singleTrain);
					sTresult = sTresult.data;
					for (var s = 0; s < schedDates.length; s++) {

						var schedEdipi = schedDates[s].EDIPI;
						var schedTrainId = schedDates[s].TRAIN_ID;
						var schedDate = schedDates[s].DATESCHED;
						schedDate = schedDate.substr(6);
						schedDate = new Date(parseInt(schedDate));
						schedDate = formatDateTime(schedDate);

						if (schedTrainId == tid && schedEdipi == curEdipi) {
							trainData[i].scheddate = schedDate;
							trainData[i].loc = loc;
						}
					}

					for (var t = 0; t < sTresult.length; t++) {
						var id = sTresult[t].TRAIN_ID;
						var edipi = sTresult[t].EDIPI;
						if (id == tid && edipi == curEdipi) {
							curTrainings.push(curTrainData);
						}
					}
				}
			}
			userTrainingArray = removeDup(curTrainings, "TRAINID");
			trainingTable(userTrainingArray);
			
		},
		"error": function (error) {
			alert("error: " + error.responseText + ", " + error.StatusText);
		}
	});
	
}

function getSingleTraining() {
	data = $.ajax({
		"url": "/Home/GetSingleTraining",
		"type": "GET",
		"dataType": "json",
		"async": false,
		"success": function (data) {
			singleTrain = data.data;
		},
		"error": function (error) {
			alert("error: " + error.responseText + ", " + error.StatusText);
		}
	})
	return data;
}

/*function getSchedDates() {
	//Get Scheduled Dates
	data = $.ajax({
		"url": "/Home/GetSchedDates",
		"type": "GET",
		"dataType": "json",
		"async": false,
		"success": function (data) {
			schedDates = data.data;
		},
		"error": function (error) {
			alert("error: " + error.responseText + ", " + error.StatusText);
		}
	})
	return data;
}*/

function getSingleSchedDates() {
	data = $.ajax({
		"url": "/Home/GetSchedDates",
		"type": "GET",
		"dataType": "json",
		"async": false,
		"success": function (data) {
			schedDates = data.data;
		},
		"error": function (error) {
			alert("error: " + error.responseText + ", " + error.StatusText);
		}
	})
	return data;
}

function getUserDates() {
	//Get Scheduled Dates
	data = $.ajax({
		"url": "/Home/GetUserDates",
		"type": "GET",
		"dataType": "json",
		"async"	: "false",
		"success": function (data) {
			userdates = data.data;
			if (userdates.length > 0) {
				curEdipi = userdates[0].EDIPI;
			}
			
			getAllSchedDates(); 
			getPocs();
			//getSchedDates();
		},
		"error": function (error) {
			alert("error: " + error.responseText + ", " + error.StatusText);
		}
	})
	return data;
}

//Remove Duplicates
function removeDup(originalArray, prop) {

	var lookupObject = {};
	newArray = [];

	for (var i in originalArray) {
		lookupObject[originalArray[i][prop]] = originalArray[i];
	}

	for (i in lookupObject) {
		newArray.push(lookupObject[i]);
	}
	return newArray;
}

// format date
function formatDate(date) {
	var d = new Date(date), month = '' + (d.getMonth() + 1), day = '' + d.getDate(), year = d.getFullYear();

	if (month.length < 2)
		month = '0' + month;
	if (day.length < 2)
		day = '0' + day;

	return [year, month, day].join('-');
}

// format date and time
function formatDateTime(date) {
	var d = new Date(date), month = '' + (d.getMonth() + 1), day = '' + d.getDate(), year = d.getFullYear(), hr = '' + (d.getHours() ), min = '' + d.getMinutes();

	if (month.length < 2)
		month = '0' + month;
	if (day.length < 2)
		day = '0' + day;
	if (min.length < 2)
		min = '0' + min;

	return [year, month, day].join('-') + " " + [hr, min].join(':');
}
// Create the trainings datatable
function trainingTable(trainData) {
	$('#noDataDisplay').css("display", "none")

	var table = $('#trainingTable').DataTable({

		dom: '1<"toolbarright">frtip',
		/*buttons: [$.extend(true, {}, buttonCommon, {
			extend: 'pdf',
			className: 'reportBtn',
			text: '<i class="fas fa-file-download"></i>',
			titleAttr: 'Selected Personnel Report',
			message: traineeName,
			content: [{
				style: 'fullWidth'
			}],
			exportOptions: {
				columns: [3, 4, 5, 6, 7, 8, 9]
			},
			customize: function (doc) {
				doc.content[2].table.widths = ['15%', '9%', '36%', '12%', '12%', '12%', '7%'];
			}
		})],*/
		initComplete: function () {
			$("div.toolbarright").html('<div class="navbar" id="nameLb"><label for="trainingFilter" class="navbar" id="lbTrainings">Trainings</label></div>');
			// .html('<div class="form-group"><label
			// for="trainingFilter" class="col-sm-2
			// control-label"
			// id="lbTrainings">Trainings</label>');

			/*if (traineeName != undefined) {
				$("div.name").html("<div id='tname'>'" + (traineeName) + "'</div>");
			} else {
				$("div.name").html("<div class='empty'><div>")
			}*/
		},

		"data": trainData,
		"sScrollY": "450",
		
		//"searching": true,
		"bJQueryUI": true,
		"destroy": true,
		"iDisplayLength": 15,
		"bPaginate": false,
		"bLengthChange": false,
		"aoColumns": [{
			"bSearchable": false,
			"bVisible": false,
			"asSorting": ["asc"]
		}],

		'columnDefs': [{
			'targets': [0, 1, 2],
			'visible': false,

		}],
		'columns': [
	
			{
				'data': "TRAINID",
				'render': function (data, type, row) {
					if (row.TRAINID != null) {
						return data;
					}
				}
				
			},
			
			{
				'data': "datetakenid",
				'render': function (data, type, row) {
					if (data != undefined) {
						return data;
					} else {
						return 0;
					}
				}
			},

			{
				'data': "edipi",
				'render': function (data, type, row) {
					if (data != undefined) {
						return data;
					} else {
						return 0;
					}
				}
			},
			{
				'data': "verified",
				'render': function (data, type, row) {
					
					var dateTaken1 = row.dateTaken;
					datenextdue = row.datenextdue;
					if (dateTaken1 != undefined) {
						//Set coming due 30 days
						if (datenextdue != undefined) {
							var comingDue = new Date(datenextdue);
							comingDue.setDate(comingDue.getDate() - 30);
							var formatedComingDue = formatDate(comingDue);
						}

						var verified = row.verified;
						var currDate = new Date();
						var formatedCurrDate = formatDate(currDate);


						if (datenextdue != null
							&& datenextdue > dateTaken1
							&& verified == "Yes"
							&& formatedCurrDate < formatedComingDue) {
							return '<i class="fas fa-circle" id = "greenIcon" style = "color:green; border:thin black solid; border-radius: 50%; margin-left: 25px;" />';
						} else if (verified != "Yes"
							&& datenextdue != null
							&& formatedCurrDate < datenextdue
							&& formatedCurrDate < formatedComingDue) {
							return '<i class="fas fa-circle" id = "yellowIcon" style = "color:yellow; border:thin black solid; border-radius: 50%; margin-left: 25px;" />';
						} else if (formatedCurrDate >= datenextdue) {
							return '<i class="fas fa-circle" id = "redIcon" style = "color:red; border:thin black solid; border-radius: 50%; margin-left: 25px;" />';
						} else if (formatedComingDue <= formatedCurrDate
							&& formatedComingDue <= datenextdue) {
							return '<i class="fas fa-circle" id = "orangeIcon" style = "color:orange; border:thin black solid; border-radius: 50%; margin-left: 25px;" />';
						} else {
							return "";
						}
					} else {
						return " ";
					}
				},
				'width': "3%",
					
			},
			{
				'data': "verified",
				'render': function (data, type, row) {
					if (type == "display"
						&& row.verified == "Yes") {
						return '<input type="checkbox" class="editor-active" name="ckbox" id="ckbox" checked="checked" disabled="disabled">';
					} else {
						if (row.dateTaken == undefined) {
							return '<input type="checkbox" class="editor-active" name="ckbox" id="ckbox" disabled="disabled">';
						} else {
							return '<input type="checkbox" class="editor-active" name="ckbox" id="ckbox" disabled="disabled">';
						}
					}
					if (data != null) {
						return data;
					}
				},
				'width': "3%",
			},

			{
				'data': "TRAIN_TITLE",
				'render': function (data, type, row) {
					if (data != null) {
						return data;
					}
				},
				'width': "29%",
			},
			{
				'data': "dateTaken",
				'render': function (data, type, row) {
					if (data != null) {
						newDate = row.dateTaken;
						return '<div class = "clickable" style = "text-align:center; height:100%; color: gray;"> '
							+ newDate + ' </div>';
					} else {
						return '<p class = "clickable" style = "text-align:center;  margin-bottom:1px;" title="Add Date & Cert">Add</p>';
					}
				},
				'className': "datetaken",
				'width': "12%",
			},
			{
				'data': "datenextdue",
				'render': function (data, type, row) {
					if (data != null) {
						var newDate = row.datenextdue;
						return '<div style = "height:100%; cursor:not-allowed; text-align:center;" readonly> '
							+ newDate + ' </div>';
					} else {
						return "";
					}
				},
				"width": "11%",
			},
			{
				'data': "datenextdue",
				'render': function (data, type, row) {
					if (data != null) {
						var newDate = new Date(row.datenextdue);
						var curDate = new Date();
						//curDate = formatDate(curDate);
						var differenceInTime = Math.round(newDate.getTime() - curDate.getTime());
						var differenceInDays = Math.round(differenceInTime / (1000 * 3600 * 24))
						if (differenceInTime.toString() != "NaN") {
							return '<div style = "height:100%; text-align:center; cursor:not-allowed;" readonly> '
								+ differenceInDays + "  " + " Days " + ' </div>';
						} else {
							return '<p style="text-align:center;">N/A </p>'
						}
						
					} else {
						return "";
					}
				},
				"width": "12%",
			},
			{
				'data': "TYPE",
				'render': function (data, type, row) {
					var todayDate = new Date();
					var newSchedDate = new Date(row.scheddate);
					var loc = row.loc;

					if (row.TYPE != "Online") {
						if (row.scheddate != undefined && row.dateTaken == undefined) {
							if (todayDate > newSchedDate) {
								return '<p class="clickable" style = "text-align:center; cursor:pointer; color:blue;">Schedule</p>'
							} else {
								return '<p style="text-align:center;">'+ row.scheddate + '<br/>' + loc +'</p>'
							}

						} else if (row.dateTaken != undefined) {
							return '<p style = "text-align:center; cursor:not-allowed;  margin-bottom:1px;" readonly>N/A</p>'
						} else {
							return '<p style = "text-align:center; cursor:pointer; color:blue;  margin-bottom:1px;">Schedule</p>'
						}
					} else {
						return '<p style = "text-align:center; cursor:not-allowed;  margin-bottom:1px;" readonly>N/A</p>'
					}
				},
				"width": "11%",
			},
			{
				'data': "cert",
				'render': function (data, type, row) {
					if (row.dateTaken != null) {
						return '<a href="#" data-toggle="tooltip" title="View Cert"><i class="far fa-file-pdf" style = "color: #0033cc; font-size: 21px; cursor:pointer; margin-left: 15px;"/></a>'
					} else {
						return "";
					}
				},
				'width': "5%"
			}

		],
		'rowCallback': function (row, data, index) {
			var datetaken = data.dateTaken;
			var nextdue = data.datenextdue;
			var type = data.TYPE;
			var scheddate = data.scheddate;

			if (type == "In-Person") {
				$(row).find('td:eq(5)').css('color', 'blue').css('cursor', 'pointer');
			}
			if (type == "In-Person" && scheddate != undefined) {
				$(row).find('td:eq(5)').css('color', 'gray').css('cursor', 'not-allowed');
			}
			// Colors:
			// #c6ecc6 = Green
			// #ffb3b3 = Red
			// #0033cc = Blue
			// Set color on DateTaken Column
			if (datetaken == undefined) {
				$(row).find('td:eq(3)').css('color', '#0033cc').css('cursor', 'pointer');
				return;
			} else {
				$(row).find('td:eq(0)').css('color', 'blue');
			}
			// Get row ID
			var rowId = data.trainid;

		},
	});
}
$(function () {
	$('#classDate').pickadate(
		{
			// The title label to use for the month nav buttons
			labelMonthNext: 'Next month',
			labelMonthPrev: 'Previous month',

			// The title label to use for the dropdown selectors
			labelMonthSelect: 'Select a month',
			labelYearSelect: 'Select a year',

			// Months and weekdays
			monthsFull: ['January', 'February', 'March', 'April', 'May',
				'June', 'July', 'August', 'September', 'October',
				'November', 'December'],
			monthsShort: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun',
				'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
			weekdaysFull: ['Sunday', 'Monday', 'Tuesday', 'Wednesday',
				'Thursday', 'Friday', 'Saturday'],
			weekdaysShort: ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri',
				'Sat'],

			// Today and clear
			today: 'Today',
			clear: 'Clear',
			close: 'Close',

			// Picker close behavior
			closeOnSelect: true,
			closeOnClear: true,

			// Update input value on select/clear
			updateInput: true,

			// The format to show on the `input` element
			format: 'd mmmm, yyyy',

		});

	$('#classTime').pickatime({
		// Clear
		clear: 'Clear',

		// The format to show on the `input` element
		format: 'h:i A',

		// The interval between each time
		interval: 30,

		// Picker close behavior
		closeOnSelect: true,
		closeOnClear: true,

		// Update input value on select/clear
		updateInput: true,
	})
});
$(function () {
	$('#clasDate').pickadate(
		{
			// The title label to use for the month nav buttons
			labelMonthNext: 'Next month',
			labelMonthPrev: 'Previous month',

			// The title label to use for the dropdown selectors
			labelMonthSelect: 'Select a month',
			labelYearSelect: 'Select a year',

			// Months and weekdays
			monthsFull: ['January', 'February', 'March', 'April', 'May',
				'June', 'July', 'August', 'September', 'October',
				'November', 'December'],
			monthsShort: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun',
				'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
			weekdaysFull: ['Sunday', 'Monday', 'Tuesday', 'Wednesday',
				'Thursday', 'Friday', 'Saturday'],
			weekdaysShort: ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri',
				'Sat'],

			// Today and clear
			today: 'Today',
			clear: 'Clear',
			close: 'Close',

			// Picker close behavior
			closeOnSelect: true,
			closeOnClear: true,

			// Update input value on select/clear
			updateInput: true,

			// The format to show on the `input` element
			format: 'd mmmm, yyyy',

		});

	$('#classTime').pickatime({
		// Clear
		clear: 'Clear',

		// The format to show on the `input` element
		format: 'h:i A',

		// The interval between each time
		interval: 30,

		// Picker close behavior
		closeOnSelect: true,
		closeOnClear: true,

		// Update input value on select/clear
		updateInput: true,
	})
});

