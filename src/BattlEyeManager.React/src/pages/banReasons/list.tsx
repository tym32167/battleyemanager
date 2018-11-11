import { connect } from 'react-redux';
import { Dispatch } from 'redux';
import { IdTextList } from 'src/controls';
import { IBanReason } from 'src/models';
import { banReasonActions } from "../../store/actions";

const mapStateToProps = ({ banReasons }: { banReasons: any }) => {
    return {
        createUrl: "/banReasons/create",
        editUrl: "/banReasons/",
        error: banReasons.error,
        items: banReasons.items || [],
        title: "Ban reasons:",
    }
}

const mapDispatchToProps = (dispatch: Dispatch<void>) => {
    return {
        deleteItem: (item: IBanReason) => dispatch(banReasonActions.deleteItem(item)),
        onLoad: () => dispatch(banReasonActions.getItems()),
    }
}

const ConnectedList = connect(
    mapStateToProps,
    mapDispatchToProps
)(IdTextList);

export { ConnectedList as List };
