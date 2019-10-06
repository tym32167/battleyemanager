import React, { Component } from 'react';
import { Line } from 'react-chartjs-2';
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

        // const data =
        // {
        //     labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July'],
        //     // tslint:disable-next-line: object-literal-sort-keys
        //     datasets: [
        //         {
        //             label: 'My First dataset',
        //             // tslint:disable-next-line: object-literal-sort-keys
        //             fill: false,
        //             lineTension: 0.1,
        //             backgroundColor: 'rgba(75,192,192,0.4)',
        //             borderColor: 'rgba(75,192,192,1)',
        //             // borderCapStyle: 'butt',
        //             borderDash: [],
        //             borderDashOffset: 0.0,
        //             // borderJoinStyle: 'miter',
        //             pointBorderColor: 'rgba(75,192,192,1)',
        //             pointBackgroundColor: '#fff',
        //             pointBorderWidth: 1,
        //             pointHoverRadius: 5,
        //             pointHoverBackgroundColor: 'rgba(75,192,192,1)',
        //             pointHoverBorderColor: 'rgba(220,220,220,1)',
        //             pointHoverBorderWidth: 2,
        //             pointRadius: 1,
        //             pointHitRadius: 10,
        //             data: [65, 59, 80, 81, 56, 55, 40]
        //         }
        //     ]
        // };

        return (
            <React.Fragment>
                <Error error={error} />
                <div>
                    <h2>Servers Stats</h2>
                    <Line data={forGraph} width={1000} height={600} />
                </div>
            </React.Fragment>
        );
    }
}
