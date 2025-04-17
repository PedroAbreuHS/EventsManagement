


function loadAgenda(eventsUrl) {

    var calendarEl = document.getElementById("eventAgendaId");

    var calendar = new FullCalendar.Calendar(calendarEl, {

        headerToolbar: {
            left: 'prev,next today',
            center: 'title',
            right: 'dayGridMonth,timeGridWeek,timeGridDay'
        },

        initialView: 'dayGridMonth',
        themeSystem: 'Yeti',
        contentHeight: "auto",
        events: eventsUrl,
        selectable: true,
        editable: true,
        locale: 'pt',

        buttonText: {
            prev: 'Anterior',
            next: 'Seguinte',
            month: 'Mês',
            today: 'Hoje',
            week: 'Semana',
            day: 'dia',
            list: 'Lista',
        },

        select: handleSelectEvent,

        eventClick: handleEventClick,

        eventChange: handleEventChange,

    });

    calendar.render();

    const eventModal = new bootstrap.Modal(document.getElementById("eventModal"));
    const btn = document.getElementById("btnSaveEvent");
    btn.addEventListener("click", saveEvent);

    document.getElementById("btnDeleteEvent").addEventListener("click", deleteEvent)


    function getFormattedDate(dateStr) {
        var result = "";

        if (dateStr.length >= 16) {
            result = dateStr.slice(0, 16);
        } else {
            result = dateStr.slice(0, 10) + "T00:00";
        }

        return result;
    }
    function handleSelectEvent(info) {

        document.getElementById("eventModalTitle").innerHTML = "Adicionar Evento";
        document.getElementById("eventModal_event_id").value = "";
        document.getElementById("eventModal_event_allday").value = false;
        document.getElementById("eventModal_event_title").value = "";
        document.getElementById("eventModal_event_description").value = "";
        document.getElementById("eventModal_event_start_date").value = getFormattedDate(info.startStr);
        document.getElementById("eventModal_event_end_date").value = getFormattedDate(info.endStr);

        document.getElementById("btnDeleteEvent").style.visibility = "hidden";

        eventModal.show();
    }
    function handleEventClick(info) {

        info.jsEvent.preventDefault();

        document.getElementById("eventModalTitle").innerHTML = "Editar Evento" + info.event.id;
        document.getElementById("eventModal_event_id").value = info.event.id;
        document.getElementById("eventModal_event_allday").value = info.event.allday;
        document.getElementById("eventModal_event_title").value = info.event.title;
        document.getElementById("eventModal_event_description").value = info.event.extendedProps.description;
        document.getElementById("eventModal_event_start_date").value = getFormattedDate(info.event.startStr);
        document.getElementById("eventModal_event_end_date").value = getFormattedDate(info.event.endStr);

        document.getElementById("btnDeleteEvent").style.visibility = "visible";

        eventModal.show();

    }
    function handleEventChange() {

        calendar.refetchEvents();

    }

    async function saveEvent() {
        var id = document.getElementById("eventModal_event_id").value;
        var allday = document.getElementById("eventModal_event_allday").value == "true";
        var title = document.getElementById("eventModal_event_title").value;
        var description = document.getElementById("eventModal_event_description").value;
        var start = document.getElementById("eventModal_event_start_date").value;
        var end = document.getElementById("eventModal_event_end_date").value;

        if (!end) end = null;

        try {

            var url = eventsUrl;
            var httpMethod = "POST";

            if (id.length > 0) {
                url = eventsUrl + '/' + id;
                httpMethod = "PUT"
            }

            let response = await fetch(url, {
                method: httpMethod,
                headers: { "content-Type": "application/json" },
                body: JSON.stringify({ title, description, start, end, allday })
            });

            if (response.ok) {
                eventModal.hide();

                calendar.refetchEvents();
            } else {
                alert("error");
            }

        } catch (e) {
            alert(e);
        }
    }

    async function deleteEvent() {
        var id = document.getElementById("eventModal_event_id").value;

        try {

            let response = await fetch(eventsUrl + "/" + id, {
                method: "DELETE"
            });

            if (response.ok) {
                eventModal.hide();

                calendar.refetchEvents();
            } else {
                alert("Error")
            }

        } catch (e) {
            alert(e)
        }
    }

}