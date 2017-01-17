function samplecontroller($scope) {
    $scope.status = [
        { value: 'none', text: 'none' },
        { value: 'name', text: 'Name' },
        { value: 'address', text: 'Address' },
        { value: 'all', text: 'All' }
    ];
    $scope.state = $scope.status[0];
    $scope.name = "FOO";
    $scope.address = "ADDRESS1";
}
