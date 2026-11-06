namespace ColorSort.Models {

    public record MoveRecord(
        int Source,
        int Destination,
        int Amount,
        string Color);

}
