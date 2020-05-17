import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import React, { Component } from 'react';
import { Trans } from 'react-i18next';
import { Container, Row } from 'reactstrap';
import { ISteamPlayer } from 'src/models';
import { steamStatsService } from 'src/services';
import { ClientGrid, ClientGridColumn, ClientGridColumns, Error } from '../../../controls';
import { ServerHeader } from '../onlineServerHeader';


export const SteamStatsComponent = (props: any) => (
    <React.Fragment>
        <div className="container-fluid p-lg-3 p-1">
            <div className="row">
                <div className="col-12 p-2 m-0" >
                    <div className="bg-white rounded box-shadow  p-1">
                        <ServerHeader {...props} />
                    </div>
                </div>

                <div className="col-sm-12 col-lg-11  p-2 m-0">
                    <div className="bg-white rounded box-shadow p-1">
                        <SteamStats {...props} />
                    </div>
                </div>
            </div>
        </div>
    </React.Fragment>
);

interface ISteamStatsProps {
    serverId: number,
}

interface ISteamStatsState {
    players: ISteamPlayer[],
    error: any
}


class SteamStats extends Component<ISteamStatsProps, ISteamStatsState> {
    constructor(props: ISteamStatsProps) {
        super(props);
        this.state = { players: [], error: undefined };
        this.refresh = this.refresh.bind(this);
    }

    public componentDidMount() {
        this.refresh();
    }

    public refresh() {
        const { match: { params: { serverId } } } = (this.props as any);
        steamStatsService.getPlayerStats(serverId)
            .then(resp => {
                this.setState({ ...this.state, players: resp.players, error: undefined })
            }
                , (error: any) => this.setState({ players: [], error }));

    }



    public render() {

        const { players, error } = this.state;
        const len = players ? players.length : 0;

        return (
            <React.Fragment>

                <div className="col-sm-12 col-lg-8 p-2 m-0">
                    <div className="bg-white rounded box-shadow p-1">

                        <Container fluid={true}>
                            <Row>
                                <h2><small><FontAwesomeIcon icon="sync" {...{ onClick: this.refresh }} /></small> <Trans>Refresh</Trans> ({len})</h2>
                            </Row>
                            <Row>
                                <Error error={error} />
                            </Row>
                            <Row>
                                {players &&
                                    <React.Fragment>
                                        <ClientGrid data={players} error={error} enableSort={true} enableFilter={true}
                                            sortProps={{ sortField: "name", sortDirection: false }}
                                            enableColumnManager={true}                                    >
                                            <ClientGridColumns>
                                                <ClientGridColumn header="Num" name="n" visible={true} />
                                                <ClientGridColumn header="Name" name="name" visible={true} />
                                                <ClientGridColumn header="Score" name="score" visible={true} />
                                                <ClientGridColumn header="Time" name="time" visible={true} />
                                            </ClientGridColumns>
                                        </ClientGrid>
                                    </React.Fragment>}
                            </Row>
                        </Container>
                    </div>
                </div>
            </React.Fragment>
        );
    }
}
