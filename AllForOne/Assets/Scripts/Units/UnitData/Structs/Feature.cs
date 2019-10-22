public struct Feature
{
    public string name;

    public int speed;
    public int strength;
    public int defense;
    public int cost;
    public UnitStore.BonusFeatures feat;

    public string description;
    public string featureDescription;

    //constructor
    public Feature(string name, int speed, int strength, int defense, int cost, UnitStore.BonusFeatures feat, string description, string featureDescription)
    {
        this.name = name;

        this.speed = speed;
        this.strength = strength;
        this.defense = defense;
        this.cost = cost;
        this.feat = feat;

        this.description = description;
        this.featureDescription = featureDescription;
    }
}