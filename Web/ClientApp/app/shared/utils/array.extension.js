if (!Array.prototype.remove) {
    Array.prototype.remove = function (o) {
        var index = this.indexOf(o);
        if (index !== -1) {
            this.splice(index, 1);
        }
        return this;
    }
}