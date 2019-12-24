using System;

[Serializable]
public class ArchivementData
{
    public bool Nivel1Completado = false;
    public bool Nivel2Completado = false;
    public bool JuegoCompletado = false;
    public bool BossAsesinado = false;
    public bool EnemigoSimplesAsesinados = false;
    public bool EnemigosRangoAsesinados = false;

    public int TotalEnemigosSimplesAsesinados = 0;
    public int TotalEnemigosRangoAsesinados = 0;
    public int TotalBossesEliminados = 0;

    public bool PrimeraMuerte = false;
}
