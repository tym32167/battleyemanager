import React, { Component } from 'react';
import { Line } from 'react-chartjs-2';
import { Col, Row } from 'reactstrap';
import { IServerStatsModel } from 'src/models';
import { serverStatsService } from 'src/services';
import { Error } from '../../../controls';


interface IServerStatsState {
    data: IServerStatsModel,
    error: any
}

export class ServerStats extends Component<any, IServerStatsState> {
    constructor(props: any) {
        super(props);
        this.state = { data: { dataSets: [] }, error: undefined };
    }

    public componentDidMount() {
        this.Load();
    }

    public async Load() {
        await serverStatsService.getStats().then(
            (stats: IServerStatsModel) => {
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

        if (!data.dataSets) {
            data.dataSets = [];
        }


        const forGraph = {
            ...data,
            datasets: data.dataSets!.map(x => {
                return {
                    ...x,
                    fill: false,
                    lineTension: 0.1,
                    // tslint:disable-next-line: object-literal-sort-keys
                    backgroundColor: this.RandomColor(),
                    borderColor: this.RandomColor(),
                    pointRadius: 1,
                    pointHitRadius: 10,
                }
            })
        };

        const options = {
            //  responsive: true,
            maintainAspectRatio: false
        }

        return (
            <React.Fragment>
                <Row>
                    <Col xs={12}>
                        <Error error={error} />
                        <h2>Servers Stats</h2>
                    </Col>
                </Row>

                <Row className="chartContainer">
                    <Col xs={12}>
                        <Line data={forGraph} options={options} />
                    </Col>
                </Row>
            </React.Fragment>
        );
    }
}
