export class Task {
    constructor(
        public id: any,
        public name: string,
        public type: string,
        public description: string,
        public softwareengineerid: number,
        public softwareEngineerFirstName: string,
        public softwareEngineerLastName: string) { }

}

export class Employee {
    constructor(
        public id: any,
        public firstName: string,
        public lastName: string,
        public address: string,
        public email: number,
        public role: string
    ) { }

}

export class TaskDetail {
    constructor(
        public id: any,
        public description: string,
        public startDate: Date,
        public endDate: Date,
        public taskId: number,
    ) { }

}