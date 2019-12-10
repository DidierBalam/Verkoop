am4core.ready(function () {
    Productos();
    Categoria();
    Usuarios();
});

var Productos = function () {
    am4core.ready(function () {

        // Themes begin
        am4core.useTheme(am4themes_animated);
        // Themes end

        /**
         * Chart design taken from Samsung health app
         */

        var chart = am4core.create("Producchartdiv", am4charts.XYChart);
        chart.hiddenState.properties.opacity = 0; // this creates initial fade-in

        chart.data = [{
            "date": "2018-01-01",
            "steps": 9867
        }, {
            "date": "2018-01-02",
            "steps": 7561
        }, {
            "date": "2018-01-03",
            "steps": 6348
        }, {
            "date": "2018-01-04",
            "steps": 5697
        }, {
            "date": "2018-01-05",
            "steps": 5687
        }, {
            "date": "2018-01-06",
            "steps": 4878
        }, {
            "date": "2018-01-07",
            "steps": 4878
        }, {
            "date": "2018-01-08",
            "steps": 4561
        }, {
            "date": "2018-01-09",
            "steps": 3298
        }, {
            "date": "2018-01-10",
            "steps": 1287

        }];

        var dateAxis = chart.xAxes.push(new am4charts.DateAxis());
        dateAxis.renderer.grid.template.strokeOpacity = 0;
        dateAxis.renderer.minGridDistance = 10;
        dateAxis.dateFormats.setKey("day", "d");
        dateAxis.tooltip.hiddenState.properties.opacity = 1;
        dateAxis.tooltip.hiddenState.properties.visible = true;

        var valueAxis = chart.yAxes.push(new am4charts.ValueAxis());
        valueAxis.renderer.inside = true;
        valueAxis.renderer.labels.template.fillOpacity = 0.3;
        valueAxis.renderer.grid.template.strokeOpacity = 0;
        valueAxis.min = 0;
        valueAxis.cursorTooltipEnabled = false;

        var series = chart.series.push(new am4charts.ColumnSeries);
        series.dataFields.valueY = "steps";
        series.dataFields.dateX = "date";
        series.tooltipText = "{valueY.value}";
        series.tooltip.pointerOrientation = "vertical";
        series.tooltip.hiddenState.properties.opacity = 1;
        series.tooltip.hiddenState.properties.visible = true;
        series.tooltip.adapter.add("x", function (x, target) {
            return am4core.utils.spritePointToSvg({ x: chart.plotContainer.pixelX, y: 0 }, chart.plotContainer).x + chart.plotContainer.pixelWidth / 2;
        })

        var columnTemplate = series.columns.template;
        columnTemplate.width = 30;
        columnTemplate.column.cornerRadiusTopLeft = 20;
        columnTemplate.column.cornerRadiusTopRight = 20;
        columnTemplate.strokeOpacity = 0;

        columnTemplate.adapter.add("fill", function (fill, target) {
            var dataItem = target.dataItem;
            if (dataItem.valueY > 6000) {
                return chart.colors.getIndex(0);
            }
            else {
                return am4core.color("#a8b3b7");
            }
        })

        var cursor = new am4charts.XYCursor();
        cursor.behavior = "panX";
        chart.cursor = cursor;
        cursor.lineX.disabled = true;

        var middleLine = chart.plotContainer.createChild(am4core.Line);
        middleLine.strokeOpacity = 1;
        middleLine.stroke = am4core.color("#000000");
        middleLine.strokeDasharray = "2,2";
        middleLine.align = "center";
        middleLine.zIndex = 1;
        middleLine.adapter.add("y2", function (y2, target) {
            return target.parent.pixelHeight;
        })

        cursor.events.on("cursorpositionchanged", updateTooltip);
        dateAxis.events.on("datarangechanged", updateTooltip);

        function updateTooltip() {
            dateAxis.showTooltipAtPosition(0.5);
            series.showTooltipAtPosition(0.5, 0);
            series.tooltip.validate(); // otherwise will show other columns values for a second
        }


    }); // end am4core.ready()
};

var Categoria = function () {
    // Themes begin
    am4core.useTheme(am4themes_animated);
    // Themes end

    var chart = am4core.create("CategoriaChartdiv", am4charts.PieChart);
    chart.hiddenState.properties.opacity = 0; // this creates initial fade-in

    chart.data = [
        {
            country: "Lithuania",
            value: 401
        },
        {
            country: "Czech Republic",
            value: 300
        },
        {
            country: "Ireland",
            value: 200
        },
        {
            country: "Germany",
            value: 165
        },
        {
            country: "Australia",
            value: 139
        },
        {
            country: "Austria",
            value: 128
        }

    ];
    chart.startAngle = 180;
    chart.endAngle = 360;

    var series = chart.series.push(new am4charts.PieSeries());
    series.dataFields.value = "value";
    series.dataFields.category = "country";


    series.hiddenState.properties.startAngle = 90;
    series.hiddenState.properties.endAngle = 90;

    chart.legend = new am4charts.Legend();
};

var Usuarios = function () {
    am4core.ready(function () {

        // Themes begin
        am4core.useTheme(am4themes_animated);
        // Themes end

        // Create chart instance
        var chart = am4core.create("chartdiv", am4charts.XYChart);

        // Add data
        chart.data = [{
            "name": "John",
            "points": 35654,
            "color": chart.colors.next(),
            "bullet": "https://www.amcharts.com/lib/images/faces/A04.png"
        }, {
            "name": "Damon",
            "points": 65456,
            "color": chart.colors.next(),
            "bullet": "https://www.amcharts.com/lib/images/faces/C02.png"
        }, {
            "name": "Patrick",
            "points": 45724,
            "color": chart.colors.next(),
            "bullet": "https://www.amcharts.com/lib/images/faces/D02.png"
        }, {
            "name": "Mark",
            "points": 13654,
            "color": chart.colors.next(),
            "bullet": "https://www.amcharts.com/lib/images/faces/E01.png"
        }];

        // Create axes
        var categoryAxis = chart.xAxes.push(new am4charts.CategoryAxis());
        categoryAxis.dataFields.category = "name";
        categoryAxis.renderer.grid.template.disabled = true;
        categoryAxis.renderer.minGridDistance = 30;
        categoryAxis.renderer.inside = true;
        categoryAxis.renderer.labels.template.fill = am4core.color("#fff");
        categoryAxis.renderer.labels.template.fontSize = 20;

        var valueAxis = chart.yAxes.push(new am4charts.ValueAxis());
        valueAxis.renderer.grid.template.strokeDasharray = "4,4";
        valueAxis.renderer.labels.template.disabled = true;
        valueAxis.min = 0;

        // Do not crop bullets
        chart.maskBullets = false;

        // Remove padding
        chart.paddingBottom = 0;

        // Create series
        var series = chart.series.push(new am4charts.ColumnSeries());
        series.dataFields.valueY = "points";
        series.dataFields.categoryX = "name";
        series.columns.template.propertyFields.fill = "color";
        series.columns.template.propertyFields.stroke = "color";
        series.columns.template.column.cornerRadiusTopLeft = 15;
        series.columns.template.column.cornerRadiusTopRight = 15;
        series.columns.template.tooltipText = "{categoryX}: [bold]{valueY}[/b]";

        // Add bullets
        var bullet = series.bullets.push(new am4charts.Bullet());
        var image = bullet.createChild(am4core.Image);
        image.horizontalCenter = "middle";
        image.verticalCenter = "bottom";
        image.dy = 20;
        image.y = am4core.percent(100);
        image.propertyFields.href = "bullet";
        image.tooltipText = series.columns.template.tooltipText;
        image.propertyFields.fill = "color";
        image.filters.push(new am4core.DropShadowFilter());

    }); // end am4core.ready()
};