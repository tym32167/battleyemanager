import moment from 'moment';
import React, { Component } from 'react';
import { Line } from 'react-chartjs-2';
import { Trans } from 'react-i18next';
import { Col, Container, Row } from 'reactstrap';
import { IServerStatsGraphModel } from 'src/models';
import { Error } from '../../../controls';


interface IServerStatsState {
    data: IServerStatsGraphModel,
    error: any
}

interface IServerStatsProps {
    loader: () => Promise<IServerStatsGraphModel>,
    header: string
}

export class ServerStats extends Component<IServerStatsProps, IServerStatsState> {
    constructor(props: any) {
        super(props);
        this.state = { data: { dataSets: [] }, error: undefined };
    }

    public componentDidMount() {
        this.Load();
    }

    public async Load() {
        const { loader } = this.props;
        await loader().then(
            (stats: IServerStatsGraphModel) => {
                this.setState({ data: stats, error: undefined });
            },
            (error: any) => this.setState({ data: { dataSets: [] }, error }));
    }

    public RandomColor() {
        return 'rgba(' + this.Random() + ',' + this.Random() + ',' + this.Random() + ',1)'
    }

    public Random() {
        return Math.floor(Math.random() * 255);
    }

    public render() {
        const { data, error } = this.state;
        const { header } = this.props;

        if (!data.dataSets) {
            data.dataSets = [];
        }


        const forGraph = {
            ...data,
            datasets: data.dataSets!.map(x => {
                return {
                    ...x,
                    borderColor: this.RandomColor(),
                    fill: false,
                    lineTension: 0.1,
                    pointHitRadius: 10,
                    pointRadius: 1,
                }
            }),
            labels: (data.dates || [])!.map(x => moment.utc(x).local().format('YYYY-MM-DD HH:mm:ss')),
        };

        const options: Chart.ChartOptions = {
            //  responsive: true,
            maintainAspectRatio: false,
            scales: {
                xAxes: [{
                    ticks: {
                        autoSkip: true,
                        maxTicksLimit: 30,
                        // minRotation: 90,
                    }
                }]
            }
        }

        return (
            <React.Fragment>
                <Container>
                    <Row>
                        <Col xs={12}>
                            <Error error={error} />
                            <h2><Trans>{header}</Trans></h2>
                        </Col>
                    </Row>

                    <Row className="chartContainer">
                        <Col xs={12}>
                            <Line data={forGraph} options={options} />
                        </Col>
                    </Row>
                </Container>
            </React.Fragment>
        );
    }
}
