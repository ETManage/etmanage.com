
function DrawLine(containerID, xAxisdata, seriesdata, tooltiptxt) {
    require.config({
        paths: {
            echarts: '/Res/echarts'
        }
    });
    // 使用
    require(
        [
            'echarts',
             'echarts/theme/macarons',
            'echarts/chart/line' // 使用线状图就加载line模块，按需加载
        ],
        function (ec) {
            // 基于准备好的dom，初始化echarts图表
            var myChart = ec.init(document.getElementById(containerID));
            var option = {
                color: ['#96CFFE'],
                backgroundColor: 'rgba(0,0,0,0)', // 工具箱背景颜色
                borderColor: '#ccc',       // 工具箱边框颜色
                borderWidth: 0,            // 工具箱边框线宽，单位px，默
                tooltip: {
                    trigger: 'axis'
                },
                calculable: true,
                xAxis: [
                {
                    splitLine: {
                        show: false
                    },
                    type: 'category',
                    boundaryGap: false,
                    data: xAxisdata
                }
                ],
                yAxis: [
                {
                    splitLine: {
                        show: false
                    },
                    show: false,
                    type: 'value'
                }
                ],
                series: [
                {
                    name: tooltiptxt,
                    type: 'line',
                    itemStyle: {
                        normal: {
                            areaStyle: {
                                type: 'default'
                            }
                        }
                    },
                    lineStyle: {
                        width: 1,
                        color: '#1e90ff',
                        type: 'dashed'
                    },

                    data: seriesdata
                }
                ]
            };

            // 为echarts对象加载数据
            myChart.setOption(option);
        }
    );
}
function DrawRadar(containerID, polarindicator, seriesdata, tooltiptxt) {
    require.config({
        paths: {
            echarts: '/Res/echarts'
        }
    });
    //加载雷达图
    // 使用
    require(
        [
            'echarts',
            'echarts/theme/macarons',
            'echarts/chart/radar' // 使用线状图就加载line模块，按需加载
        ],
        function (ec) {
            // 基于准备好的dom，初始化echarts图表
            var myChart = ec.init(document.getElementById(containerID));
            var option = {
                tooltip: {
                    trigger: 'axis'
                },
                calculable: true,
                polar: [
                    {
                        indicator: polarindicator,
                        radius: 70
                    }
                ],
                series: [
                    {
                        name: tooltiptxt,
                        type: 'radar',
                        itemStyle: {
                            normal: {
                                areaStyle: {
                                    type: 'default'
                                }
                            }
                        },
                        data: seriesdata
                    }
                ]
            };
            // 为echarts对象加载数据
            myChart.setOption(option);
        }
    );
}

