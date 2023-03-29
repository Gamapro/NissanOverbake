function formatDate(date){
    if(typeof date === 'string'){
        date = new Date(date);
    }
    var dd = String(date.getDate()).padStart(2, '0');
    var mm = String(date.getMonth() + 1).padStart(2, '0'); //January is 0!
    var yyyy = date.getFullYear();
    return yyyy + '-' + mm + '-' + dd;
}

function formatDateForDisplay(date){
    if(typeof date === 'string'){
        date = new Date(date);
    }
    var dd = String(date.getDate()).padStart(2, '0');
    var mm = String(date.getMonth() + 1).padStart(2, '0'); //January is 0!
    var yyyy = date.getFullYear();
    return dd + '/' + mm + '/' + yyyy;
}

function formatDateWithNames(date){
    if(typeof date === 'string'){
        date = new Date(date);
    }
    var dd = String(date.getDate()).padStart(2, '0');
    var mm = String(date.getMonth() + 1).padStart(2, '0'); //January is 0!
    var yyyy = date.getFullYear();
    return dd + ' ' + getMonthName(mm) + ' ' + yyyy;
}

function getMonthName(month){
    var months = ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'];
    month = parseInt(month);
    return months[month - 1];
}

function getDates(){
    var currentDate = new Date();
    var tomorrow = new Date();
    tomorrow.setDate(currentDate.getDate() + 1);
    var yesterday = new Date();
    yesterday.setDate(currentDate.getDate() - 1);
    var lastWeek = new Date();
    lastWeek.setDate(currentDate.getDate() - 7);
    var lastMonth = new Date();
    lastMonth.setDate(currentDate.getDate() - 30);
    var lastYear = new Date();
    lastYear.setDate(currentDate.getDate() - 365);

    // format
    var currentDate = formatDate(currentDate);
    var tomorrow = formatDate(tomorrow);
    var yesterday = formatDate(yesterday);
    var lastWeek = formatDate(lastWeek);
    var lastMonth = formatDate(lastMonth);
    var lastYear = formatDate(lastYear);

    // current year
    var currentYear = new Date().getFullYear();

    // current month
    var currentMonth = new Date().getMonth() + 1;

    return {
        today: currentDate,
        tomorrow: tomorrow,
        yesterday: yesterday,
        lastWeek: lastWeek,
        lastMonth: lastMonth,
        lastYear: lastYear,
        todayForDisplay: formatDateForDisplay(new Date(currentDate)),
        tomorrowForDisplay: formatDateForDisplay(new Date(tomorrow)),
        yesterdayForDisplay: formatDateForDisplay(new Date(yesterday)),
        lastWeekForDisplay: formatDateForDisplay(new Date(lastWeek)),
        lastMonthForDisplay: formatDateForDisplay(new Date(lastMonth)),
        lastYearForDisplay: formatDateForDisplay(new Date(lastYear)),
        todayWithNames: formatDateWithNames(new Date(currentDate)),
        tomorrowWithNames: formatDateWithNames(new Date(tomorrow)),
        yesterdayWithNames: formatDateWithNames(new Date(yesterday)),
        lastWeekWithNames: formatDateWithNames(new Date(lastWeek)),
        lastMonthWithNames: formatDateWithNames(new Date(lastMonth)),
        lastYearWithNames: formatDateWithNames(new Date(lastYear)),
        currentYear: currentYear,
        currentMonth: currentMonth
    }
}