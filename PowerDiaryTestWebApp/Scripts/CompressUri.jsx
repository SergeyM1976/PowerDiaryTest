var CompressedRouteBox = React.createClass({
    loadRoutesFromServer: function () {
        var xhr = new XMLHttpRequest();
        xhr.open('get', this.props.url, true);
        xhr.onload = function () {
            var data = JSON.parse(xhr.responseText);
            this.setState({ data: data });
        }.bind(this);
        xhr.send();
    },
    handleRouteSubmit: function (route) {
        var data = new FormData();
        data.append('Uri', route.Uri);

        var xhr = new XMLHttpRequest();
        xhr.open('post', this.props.submitUrl, true);
        xhr.onload = function () {
            this.loadRoutesFromServer();
        }.bind(this);
        xhr.onreadystatechange = function() {
            if (xhr.readyState === XMLHttpRequest.DONE) {
                var res = JSON.parse(xhr.responseText);
                this.setState({add_result: res });
            }
        }.bind(this);
        xhr.send(data);
    },
    getInitialState: function () {
        return { data: this.props.initialData, add_result: { AlreadyExists: false, Route: { } } };
    },
    componentDidMount: function () {
        window.setInterval(this.loadRoutesFromServer, this.props.pollInterval);
    },
    render: function () {
        return (
      <div className="compressedRouteBox">
        <AddRouteForm onRouteSubmit={this.handleRouteSubmit} />        
        <AddedResult already_exists={this.state.add_result.AlreadyExists} uri={this.state.add_result.Route.Uri} 
          compressed_uri={this.state.add_result.Route.CompressedUri} compressed_uri_uv={this.state.add_result.Route.CompressedUriUserValue} />
        <h3>Routes on the server</h3>
        <RouteList data={this.state.data} />
      </div>
    );
    }
});


var AddedResult = React.createClass({
    shouldShow: function() {
        return (this.props.uri != null);
    },
    render: function () {
        if (!this.shouldShow())
            return (<p></p>);

        var header = null;
        if (this.props.already_exists) {
            header = <h4>Uri already added: </h4>;
        } else {
            header = <h4>Last added:</h4>;
        }

        return (
<div>
{header}
<p>
    <strong>Original: </strong>
    <a target="_blank" href={this.props.uri}>{this.props.uri}</a>
</p>
<p>
    <strong>Compressed: </strong>
    <a target="_blank" href={this.props.compressed_uri}>{this.props.compressed_uri_uv}</a>
</p>
</div>);}
});


var RouteList = React.createClass({
    render: function () {
        var routeNodes = this.props.data.map(function (route) {
            return (
                <Route uri={route.Uri} key={route.Id} compressed_uri={route.CompressedUri} compressed_uri_uv={route.CompressedUriUserValue} />
            );
        });
        return (
<table className="routeList table table-striped">
    <thead>
        <tr>
            <th>Uri</th>
            <th>Compressed Uri</th>
        </tr>
    </thead>
<tbody>
    {routeNodes}
</tbody>
    
</table>
    );
    }
});





var Route = React.createClass({
    maxLengthToShow: 100,
    getUriUserValue: function () {                
        var v = this.props.uri;
        if (v.length > this.maxLengthToShow) {
            v = v.substr(0, this.maxLengthToShow) + "...";
        }
        return v;
    },
    render: function () {
        return (
<tr className="route">
    <td><a target="_blank" href={this.props.uri }>{this.getUriUserValue()}</a></td>
    <td><a target="_blank" href={this.props.compressed_uri }>{this.props.compressed_uri_uv}</a></td>
</tr>
    );
    }
});


var AddRouteForm = React.createClass({
    getInitialState: function () {
        return { uri: ''};
    },
    handleUriChange: function (e) {
        this.setState({ uri: e.target.value });
    },
    handleSubmit: function (e) {
        e.preventDefault();
        var uri = this.state.uri.trim();
        if (!uri) {
            return;
        }
        this.props.onRouteSubmit({ Uri: uri});
        this.setState({ uri: ''});
    },
    render: function () {
        return (
<form className="addRouteForm" onSubmit={this.handleSubmit}>
    <div className="form-group">
        <input className="form-control" type="text"
            placeholder="Enter URI to compress..."
            value={this.state.uri}
            onChange={this.handleUriChange} />
    </div>
    <input className="btn btn-default" type="submit" value="Add" />
</form>
      );
    }
});

