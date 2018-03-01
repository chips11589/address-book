interface Array<T> {
    remove(elem: T): Array<T>;
}

if (!Array.prototype.remove) {
    Array.prototype.remove = function (o) {
        const index: number = this.indexOf(o);
        if (index !== -1) {
            this.splice(index, 1);
        }
        return this;
    }
}