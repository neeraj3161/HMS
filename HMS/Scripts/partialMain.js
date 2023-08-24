$("#menu-toggle").click(function (e) {
	console.log("called");
	e.preventDefault();
	$("#wrapper").toggleClass("toggled");
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


var currentActiveTab;
const dashboard = $(".dash");
const search = $(".search");
const records = $(".records");
const appointments = $(".appointments");
const support = $(".support");
const profile = $(".profile");

//show dashboard by default
hideAllExcept("dashboard");
toastr.options.closeButton = true;
toastr.success('HMS Dashboard', 'Welcome to HMS');


function hideAllExcept(exceptionViewName)
{
	switch (exceptionViewName)
	{
		case "dashboard":
			dashboard.show();
			search.hide();
			records.hide();
			appointments.hide();
			support.hide();
			profile.hide();
			break;
		case "search":
			dashboard.hide();
			search.show();
			records.hide();
			appointments.hide();
			support.hide();
			profile.hide();
			break;
		case "records":
			dashboard.hide();
			search.hide();
			records.show();
			appointments.hide();
			support.hide();
			profile.hide();
			break;
		case "appointments":
			dashboard.hide();
			search.hide();
			records.hide();
			appointments.show();
			support.hide();
			profile.hide();
			break;
		case "support":
			dashboard.hide();
			search.hide();
			records.hide();
			appointments.hide();
			support.show();
			profile.hide();
			break;
		case "profile":
			dashboard.hide();
			search.hide();
			records.hide();
			appointments.hide();
			support.hide();
			profile.show();
			break;
	}
}

function HospitalInfoModal(Id, Name, Description, ContactNumber, Address, State, Type)
{
	this.Id = Id;
	this.Name = Name;
	this.Description = Description;
	this.ContactNumber = ContactNumber;
	this.Address = Address;
	this.State = State;
	this.Type = Type;
	this.Type === 1 ? this.Type = "Govt" : this.Type ="Pvt";
}

$('.dashboardBtn').click(function (e) {
	hideAllExcept("dashboard");
	currentActiveTab = 'dashboard';
})

$('.searchBtn').click(function (e)
{
	hideAllExcept("search");
	let hospitalInfoData = [];
	$.ajax({
		url: "/PatientMain/GetAllHospitalInfo", // URL to the API endpoint
		type: "POST",                         // HTTP method
		contentType: "application/json",      // Content type
		data: '',    // Data to be sent
		success: function (response) {
			if (response.success)
			{
				if (response.result.length == 0) {
					toastr.warning('No result found', 'Error');
				}
				else
				{
					$(response.result).each(function () {
						hospitalInfoData.push(new HospitalInfoModal(this.HospitalId, this.Name, this.Description, this.PhoneNo, this.Address, this.State, this.HospitalType));
					});
					toastr.success('Data loaded successfully', 'Success');
					
					if (currentActiveTab != "search")
					{
						const table = $('#hospitalInfoTable').DataTable({
							data: hospitalInfoData,
							buttons: [
							'copy', 'excel', 'pdf'
						],
							select: true,
							columns: [
								{ data: 'Id' },
								{ data: 'Name' },
								{ data: 'Description' },
								{ data: 'ContactNumber' },
								{ data: 'Address' },
								{ data: 'State' },
								{ data: 'Type' }
							],
							//set page length from 10 to 5
							pageLength: 5,
							lengthMenu: [[5, 10, 20, -1], [5, 10, 20, 'Todos']]
						});
						$("#table tr").css('cursor', 'pointer');
						$('#hospitalInfoTable tbody').on('click', 'tr', function () {
							console.log(table.row(this).data().Id);
							$.ajax({
								url: "/PatientMain/HospitalInfo", // URL to the API endpoint
								type: "POST",                         // HTTP method
								contentType: "application/json",      // Content type
								data: JSON.stringify({ id: table.row(this).data().Id}),    // Data to be sent
								success: function (response) {
									window.location.href = "/PatientMain/HospitalInfo"
								},
								error: function (xhr, status, error) {
									console.log("Error:", error);
								}
							});
						});

					}
					currentActiveTab = 'search';

				}

			}
			
		},
		error: function (xhr, status, error) {
			console.log("Error:", error);
		}
	});

})

$('.recordsBtn').click(function (e) {
	console.log("records");
	hideAllExcept("records");

})


$('.appointmentsBtn').click(function (e) {
	hideAllExcept("appointments");

})

$('.profileBtn').click(function (e) {
	hideAllExcept("profile");

})

$('.supportBtn').click(function (e) {
	hideAllExcept("support");

})

$('.logoutBtn').click(function (e) {
	$.ajax({
		url: "/PatientMain/Logout", // URL to the API endpoint
		type: "POST",                         // HTTP method
		contentType: "application/json",      // Content type
		data: '',    // Data to be sent
		success: function (response) {
			if (response.success) {
				window.location.href = "/";
			}
			else {
				toastr.error("Error occurred while logging out", "Please try again");
			}
		},
		error: function (xhr, status, error) {
			console.log("Error:", error);
		}
	});
})



