$(document).ready(function () {
	const table=$('#hospitalInfoPage').DataTable({
		buttons: [
			'copy', 'excel', 'pdf'
		],
		select: true,
		//set page length from 10 to 5
		pageLength: 5,
		lengthMenu: [[5, 10, 20, -1], [5, 10, 20, 'Todos']]
	});

	function getSpecialtyNumber(specialtyName) {
		switch (specialtyName) {
			case "Cardiology": return 1;
			case "Neurology": return 2;
			case "Orthopedics": return 3;
			case "Gastroenterology": return 4;
			case "Dermatology": return 5;
			case "Pediatrics": return 6;
			case "ObstetricsAndGynecology": return 7;
			case "Ophthalmology": return 8;
			case "Urology": return 9;
			case "Nephrology": return 10;
			case "Pulmonology": return 11;
			case "Endocrinology": return 12;
			case "Hematology": return 13;
			case "Rheumatology": return 14;
			case "Oncology": return 15;
			case "InfectiousDiseases": return 16;
			case "AllergyAndImmunology": return 17;
			case "Psychiatry": return 18;
			case "Radiology": return 19;
			case "Anesthesiology": return 20;
			case "GeneralSurgery": return 21;
			case "PlasticSurgery": return 22;
			case "Otolaryngology": return 23;
			case "EmergencyMedicine": return 24;
			case "FamilyMedicine": return 25;
			case "InternalMedicine": return 26;
			case "PhysicalMedicineAndRehabilitation": return 27;
			case "NuclearMedicine": return 28;
			case "Pathology": return 29;
			case "CriticalCareMedicine": return 30;
			case "Geriatrics": return 31;
			case "PreventiveMedicine": return 32;
			case "OccupationalMedicine": return 33;
			case "SleepMedicine": return 34;
			case "SportsMedicine": return 35;
			case "PalliativeCare": return 36;
			case "IntegrativeMedicine": return 37;
			case "TravelMedicine": return 38;
			case "AddictionMedicine": return 39;
			case "PainMedicine": return 40;
			case "MedicalGenetics": return 41;
			case "ClinicalPharmacology": return 42;
			case "AerospaceMedicine": return 43;
			case "ForensicMedicine": return 44;
			case "RehabilitationMedicine": return 45;
			case "CommunityMedicine": return 46;
			case "DisasterMedicine": return 47;
			case "TropicalMedicine": return 48;
			case "VeterinaryMedicine": return 49;
			case "Dentistry": return 50;
			case "Podiatry": return 51;
			case "Optometry": return 52;
			case "Naturopathy": return 53;
			case "Chiropractic": return 54;
			case "Homeopathy": return 55;
			case "Acupuncture": return 56;
			case "Osteopathy": return 57;
			case "GeneticCounseling": return 58;
			case "MedicalWriting": return 59;
			case "MedicalIllustration": return 60;
			default: return 61; // Other
		}
	}

	const patientId = $('#patientId');
	const doctorId = $('#doctorId');
	const departmentId = $('#departmentId');
	const age = $('#age');
	const symptoms = $('#symptoms');
	const patIdNum = $('#patIdNum');
	const hospitalId = $('#hospitalIdInp');
	const hptId = $('#hptId');

	var doctor_id;
	var dept_id;
	$('#hospitalInfoPage tbody').on('click', 'tr', function () {
		console.log(table.row(this).data());
		let tableData = table.row(this).data();
		patientId.val(patIdNum.text());
		doctor_id = tableData[0];
		doctorId.val(tableData[0]);
		departmentId.val(getSpecialtyNumber(tableData[2]));
		dept_id = getSpecialtyNumber(tableData[2]);
		hospitalId.val(hptId.text());
	});

	$("#submitAppointmentBtn").click(function (e) {
		$.ajax({
			url: "/PatientMain/InsertAppointment", // URL to the API endpoint
			type: "POST",                         // HTTP method
			contentType: "application/json",      // Content type
			data: JSON.stringify({
				patientId: patIdNum.text(),
				doctor_id: doctor_id,
				department_id: dept_id,
				hospital_id: hptId.text(),
				treatmentInfo: $('#symptoms').val(),
				age: $('#age').val()
			}),    // Data to be sent
			success: function (response) {
				if (response) {
					$('.modal').hide();
					toastr.success("Appointment added successfully", "Success");
				} else {
					$('.modal').hide();
					toastr.error("There was a error while adding appointment", "Please try again");
				}
			},
			error: function (xhr, status, error) {
				console.log("Error:", error);
			}
		});
	})
});